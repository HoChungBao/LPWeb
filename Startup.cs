using System;
using System.IO;
using LienPhatERP.Contants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LienPhatERP.Data;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using LienPhatERP.Models;

using LienPhatERP.Notification;
using LienPhatERP.Scheduler;

using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace LienPhatERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LienPhatERPDBConnectionString")));
            services.AddDbContext<MediGroupContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LienPhatERPDBConnectionString")));
            services.AddIdentity<ApplicationUser, IdentityRole>()

                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //    })
            //    .AddJwtBearer(cfg =>
            //    {
            //        cfg.RequireHttpsMetadata = false;
            //        cfg.SaveToken = true;
            //        cfg.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = Configuration["Jwt:JwtIssuer"],
            //            ValidAudience = Configuration["Jwt:JwtIssuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:JwtKey"])),
            //            ClockSkew = TimeSpan.Zero // remove delay of token when expire
            //        };
            //    });

            // Add application services.
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IInformationService, InformationService>(); 
            services.AddScoped<IOutletStoreService, OutletStoreService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IScheduleSendmailJob, ScheduleSendmailJob>();
           
            //services.AddSingleton<IHostedService, ScheduleTask>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddMvc();
            services.AddResponseCaching();

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("default", new CacheProfile
                {
                    Duration = 120,
                    Location = ResponseCacheLocation.Any
                });
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "image/svg+xml",
                    "image/png",
                    "image/jpeg",
                    "image/jpg"

                };
                options.EnableForHttps = true;
            });
            services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Hrm", policy =>
                    policy.RequireRole(RoleBase.Hrm.ToString()));
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));

                options.AddPolicy("RequireHospitalAprove", policy =>
                    policy.RequireRole(RoleBase.Admin.ToString(), RoleBase.HospitalCoodinator.ToString(),
                        RoleBase.Accountant.ToString(), RoleBase.HospitalPartner.ToString()));
                options.AddPolicy("RequireInputData", policy =>
                    policy.RequireRole(RoleBase.Admin.ToString(), RoleBase.HospitalCoodinator.ToString(),
                        RoleBase.Accountant.ToString(), RoleBase.SystemOperator.ToString()));
                options.AddPolicy("RequireContent", policy =>
                    policy.RequireRole(RoleBase.Admin.ToString(), RoleBase.Content.ToString(),RoleBase.Marketing.ToString()));
                options.AddPolicy("RequireOrderRole", policy =>
                    policy.RequireRole(RoleBase.Admin.ToString(), RoleBase.ClientService.ToString(),
                        RoleBase.Accountant.ToString(), RoleBase.SystemOperator.ToString(), RoleBase.Sale.ToString()));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditPolicy", policy =>
                    policy.Requirements.Add(new SameAuthorRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationCrudHandler>();
            //services.AddSingleton<IAuthorizationHandler, CanDoAuthorizationCrudHandler>();
            services.AddSignalR(options =>
            {
                //options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromMilliseconds(1234);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-Powered-By");

                context.Response.Headers.Add("X-Frame-Options", "ALLOW-FROM https://www.facebook.com/");
                await next();
            });
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "UploadFile")),
                RequestPath = new PathString("/UploadFile")
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "assets")),
                RequestPath = new PathString("/assets")
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "MediHubTheme")),
                RequestPath = new PathString("/MediHubTheme")
            });
            //  app.UseFileServer();
            //app.UseFileServer(enableDirectoryBrowsing: true);
            //app.UseFileServer(new FileServerOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "UploadFiles")),
            //    RequestPath = new PathString("/UploadFiles"),
            //    EnableDirectoryBrowsing = true
            //});
            app.UseAuthentication();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AspNetUsers, MemberViewModel>();
                cfg.CreateMap<AspNetRoles, RoleViewModel>();
                cfg.CreateMap<Orders, OrderViewModel>();
                cfg.CreateMap<OrderModel, Orders>();
                cfg.CreateMap<CsCreens, CsreenModel>();
                cfg.CreateMap<CsreenModel, CsreenModel>();
                cfg.CreateMap<ContactFormPlan, ContactFormPlanModel>();
                cfg.CreateMap<Category, CategotyViewModel>();
                cfg.CreateMap<NewsPostViewModel, NewsPost>();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<Contract, ContractModel>().ForMember(d => d.PaymentContract, opt => opt.Ignore());

            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Guest",
                    template: "danh-sach",
                    defaults: new { controller = "Home", action = "Dashboard" });
                routes.MapRoute(
                    name: "Eng",
                    template: "Eng",
                    defaults: new { controller = "Home", action = "HomeEng" });
                routes.MapRoute(
                  name: "Eng/Service",
                  template: "Eng/Service",
                  defaults: new { controller = "Home", action = "ServiceEng" });
                //routes.MapRoute(
                //    name: "gio-hang",
                //    template: "Orders/gio-hang",
                //    defaults: new { controller = "Orders", action = "CreateNewOrder" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
