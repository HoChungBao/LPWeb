using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace LienPhatERP.Controllers
{
    [Produces("application/json")]
    [Route("api/CommonApi")]
    public class CommonApiController : Controller
    {
        private IHostingEnvironment _env;
        private readonly IScheduleSendmailJob _scheduleSendmailJob;
        private IEmailSender _emailSender;
        private IConfiguration _config;
    
        private ICommonService _commonService;
        private ICustomerService _customerService;
        private readonly ILogger _logger;
        public CommonApiController(IHostingEnvironment env, IScheduleSendmailJob scheduleSendmailJo,
            IEmailSender emailSender, IConfiguration config, ICommonService commonService, ICustomerService customerService,
        ILogger<AccountController> logger)
        {
            _env = env;
            _scheduleSendmailJob = scheduleSendmailJo;
            _emailSender = emailSender;
            _config = config;
            _commonService = commonService;
            _customerService = customerService;
            _logger = logger;
        }
        [HttpGet("Sendmail/{token}")]
        public IActionResult Sendmail(string token)
        {
            var urldomain = Request.Scheme + "://" + Request.Host.Value;
            if (string.IsNullOrEmpty(token)) return BadRequest();
            else
            {
                if (!token.Equals("MEDIHUB1@23456")) return BadRequest();
                var rs = _scheduleSendmailJob.SendMailContractAsync();
                
                if (rs.Count > 0)
                {
                    foreach (var item in rs)
                    {
                        
                        var url =
                            $"{urldomain}/Customer/ContractById?id={item.CustomerId}&userId={item.UserCreatedNavigation?.Email}";
                        var messageBodyCus = "";
                        MessageBody("NotifyContract.html", out messageBodyCus);
                        messageBodyCus = messageBodyCus.Replace("{0}", item.Name.ToString())
                            .Replace("{1}", item.Customer.Phone)
                            .Replace("{2}", item.EndDate.Value.ToString("dd-MM-yyyy HH:mm:ss"))
                            .Replace("{3}", url)
                            .Replace("{4}", "Thông báo sắp hết hạn Hợp đồng");

                        _emailSender.SendEmailAsync("nvthintt@gmail.com", "Mail thông báo tự động", messageBodyCus);
                    }
                    var list = rs.Select(x => new EmaiSended
                    {
                        Name = "EMAILTUDONG",
                        CreatedDate = DateTime.Now,
                        ForId = x.Id,
                        Type = TypeEmail.CONTRACT_WARNING.ToString()
                    }).ToList();
                    _scheduleSendmailJob.InsertMailSended(list);
                }
                var newContactForm = _scheduleSendmailJob.SendMailContactFormAsync();
                if (newContactForm.Count > 0)
                {
                    foreach (var item in newContactForm)
                    {
                       
                        var url =
                            $"{urldomain}/Orders/ContactFormPlan";
                        var messageBodyCus = "";
                        MessageBody("NotifyContract.html", out messageBodyCus);
                        messageBodyCus = messageBodyCus.Replace("{0}", item.Name.ToString())
                            .Replace("{1}", item.Phone.ToString())
                            .Replace("{2}", item.DateCreated.ToString("dd-MM-yyyy HH:mm:ss"))
                            .Replace("{3}", url
                            .Replace("{4}", "Thông tin khách hàng liên hệ"));

                        _emailSender.SendEmailAsync(_config["Email:Sales"], "Mail thông báo tự động", messageBodyCus);
                    }
                    var list = newContactForm.Select(x => new EmaiSended
                    {
                        Name = "EMAILTUDONG",
                        CreatedDate = DateTime.Now,
                        ForId = x.Id,
                        Type = TypeEmail.CONTACTFORM_NEW.ToString()
                    }).ToList();
                   
                    _scheduleSendmailJob.InsertMailSended(list);
                }
                return Ok();
            }
          
        }
        [HttpPost("InsertContact")]
        public IActionResult InsertContact([FromBody]ContactFormPlanModel vm)
        {
            var rs = new HttpContentResult<dynamic>
            {
                Result = false,
                StatusCode = "404"
            };
            try
            {
                var data = _customerService.ContactFormPlan(Mapper.Map<ContactFormPlan>(vm));

                if (data != null)
                {
                    var urldomain = Request.Scheme + "://" + Request.Host.Value + $"/Customer/AllContact";
                    var messageBodyCus = "";
                    _emailSender.MessageBody("NotifyContract.html", out messageBodyCus);
                    messageBodyCus = messageBodyCus.Replace("{0}", data.Name)
                        .Replace("{1}", data.Phone)

                        .Replace("{2}", data.DateCreated.ToString("dd-MM-yyyy HH:mm:ss"))
                        .Replace("{3}", urldomain);
                      
                     _emailSender.SendEmailAsync(_config["Email:SaleExcutive"], "Mail thông báo tự động",
                        messageBodyCus); // order --> send mail cho client service     
                    rs.Result = true;
                   
                    rs.Message = "Thành Công";
                    rs.StatusCode = "200";

                    return Ok(rs);
                }
                rs.Message = "Yêu cầu không hợp lệ";
                return Ok(rs);

            }
            catch (Exception ex)
            {
                rs.SysMessage = ex.ToString();
                rs.Message = ResultStatus.Fail;
                return Ok(rs);
            }
        }

        private void MessageBody(string templateEmail, out string messageBody)
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "Templates"
                             + Path.DirectorySeparatorChar.ToString()
                             + "EmailTemplate"
                             + Path.DirectorySeparatorChar.ToString()
                             + templateEmail;
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            messageBody = builder.HtmlBody;
        }

      

    }
}