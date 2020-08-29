using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LienPhatERP.Entities
{
    public partial class MediGroupContext : DbContext
    {
        public MediGroupContext(DbContextOptions<MediGroupContext> options)
            : base(options)
        { }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<BookingMeetingRoom> BookingMeetingRoom { get; set; }
        public virtual DbSet<BusinessTrip> BusinessTrip { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ComboDetails> ComboDetails { get; set; }
        public virtual DbSet<Combos> Combos { get; set; }
        public virtual DbSet<ContactFormPlan> ContactFormPlan { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<CsCreens> CsCreens { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DictionaryData> DictionaryData { get; set; }
        public virtual DbSet<EContactForm> EContactForm { get; set; }
        public virtual DbSet<EmaiSended> EmaiSended { get; set; }
        public virtual DbSet<EProduct> EProduct { get; set; }
        public virtual DbSet<Infomation> Infomation { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<LogAction> LogAction { get; set; }
        public virtual DbSet<MComponent> MComponent { get; set; }
        public virtual DbSet<MDrugLabel> MDrugLabel { get; set; }
        public virtual DbSet<MOutletStore> MOutletStore { get; set; }
        public virtual DbSet<MOutletStoreUser> MOutletStoreUser { get; set; }
        public virtual DbSet<MProject> MProject { get; set; }
        public virtual DbSet<MProjectDetail> MProjectDetail { get; set; }
        public virtual DbSet<NewsPost> NewsPost { get; set; }
        public virtual DbSet<NewsPostCategory> NewsPostCategory { get; set; }
        public virtual DbSet<PaymentContract> PaymentContract { get; set; }
        public virtual DbSet<RegisterDateOff> RegisterDateOff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=DESKTOP-M67QVLB;Database=MediGroup;persist security info=True;user id=sa;password=123456");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Bank).HasMaxLength(256);

                entity.Property(e => e.CompanyName).HasMaxLength(256);

                entity.Property(e => e.ContractType).HasMaxLength(256);

                entity.Property(e => e.CurrentContract).HasMaxLength(256);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateWork).HasColumnType("datetime");

                entity.Property(e => e.District).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Folk).HasMaxLength(256);

                entity.Property(e => e.GraduationYear).HasMaxLength(256);

                entity.Property(e => e.Home).HasMaxLength(256);

                entity.Property(e => e.IdentityCard).HasMaxLength(256);

                entity.Property(e => e.IssuedBy).HasMaxLength(256);

                entity.Property(e => e.IssuedDate).HasColumnType("datetime");

                entity.Property(e => e.Level).HasMaxLength(256);

                entity.Property(e => e.Male).HasMaxLength(256);

                entity.Property(e => e.Matrimony).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NoBhxh)
                    .HasColumnName("NoBHXH")
                    .HasMaxLength(256);

                entity.Property(e => e.NoContract).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Position).HasMaxLength(256);

                entity.Property(e => e.Province).HasMaxLength(256);

                entity.Property(e => e.Salary).HasMaxLength(256);

                entity.Property(e => e.School).HasMaxLength(256);

                entity.Property(e => e.Tgbh)
                    .HasColumnName("TGBH")
                    .HasMaxLength(256);

                entity.Property(e => e.Tghd)
                    .HasColumnName("TGHD")
                    .HasMaxLength(256);

                entity.Property(e => e.UserCode).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<BookingMeetingRoom>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Hour)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserCreated)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.UserCreatedNavigation)
                    .WithMany(p => p.BookingMeetingRoom)
                    .HasForeignKey(d => d.UserCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingMeetingRoom_AspNetUsers");
            });

            modelBuilder.Entity<BusinessTrip>(entity =>
            {
                entity.Property(e => e.Contact).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Customer).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Timer).HasMaxLength(100);

                entity.Property(e => e.UserApproved).HasMaxLength(450);

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.HasOne(d => d.UserCreatedNavigation)
                    .WithMany(p => p.BusinessTrip)
                    .HasForeignKey(d => d.UserCreated)
                    .HasConstraintName("FK_BusinessTrip_AspNetUsers");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(200);

                entity.Property(e => e.MetaDescription).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Path).HasMaxLength(2500);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InverseCategoryNavigation)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<ComboDetails>(entity =>
            {
                entity.Property(e => e.CsScreenName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PriceFull).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Combos>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Speciality)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ContactFormPlan>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

               

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

               
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.NoContract).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TotalMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.UserCreatedNavigation)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.UserCreated)
                    .HasConstraintName("FK_Contract_AspNetUsers");
            });

            modelBuilder.Entity<CsCreens>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.AdvertiseProductsType).HasMaxLength(500);

                entity.Property(e => e.District).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Phone).HasMaxLength(200);

                entity.Property(e => e.PriceMonopoly).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceWeekly).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Province).HasMaxLength(200);

                entity.Property(e => e.Speciality).HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Contact).HasMaxLength(200);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(200);

                entity.Property(e => e.Pic)
                    .HasColumnName("PIC")
                    .HasMaxLength(200);

                entity.Property(e => e.Status).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.Property(e => e.UserPic).HasMaxLength(450);

                entity.HasOne(d => d.UserPicNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.UserPic)
                    .HasConstraintName("FK_Customer_AspNetUsers");
            });

            modelBuilder.Entity<DictionaryData>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<EContactForm>(entity =>
            {
                entity.ToTable("E_ContactForm");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(500);

                entity.Property(e => e.UserUpdate).HasMaxLength(450);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.EContactForm)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_E_ContactForm_E_Product");
            });

            modelBuilder.Entity<EmaiSended>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<EProduct>(entity =>
            {
                entity.ToTable("E_Product");

                entity.Property(e => e.CreatedById).HasMaxLength(450);

                entity.Property(e => e.UpdatedById).HasMaxLength(450);
            });

            modelBuilder.Entity<Infomation>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.Email).HasMaxLength(400);

                entity.Property(e => e.Phone).HasMaxLength(400);
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Files_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FilesNavigation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_File_AspNetUsers");
            });

            modelBuilder.Entity<LogAction>(entity =>
            {
                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MComponent>(entity =>
            {
                entity.ToTable("M_Component");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDrugLabel>(entity =>
            {
                entity.ToTable("M_DrugLabel");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MOutletStore>(entity =>
            {
                entity.ToTable("M_OutletStore");

                entity.Property(e => e.Area).HasMaxLength(400);

                entity.Property(e => e.AvatarUrl).HasMaxLength(500);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.District).HasMaxLength(200);

                entity.Property(e => e.DistrictCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DrugStoreAddress).HasMaxLength(500);

                entity.Property(e => e.DrugStoreName).HasMaxLength(150);

                entity.Property(e => e.Latitue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longtitue).HasMaxLength(50);

                entity.Property(e => e.MediGroupCode).HasMaxLength(128);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Province).HasMaxLength(200);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SizeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StandardLevelCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreOwner).HasMaxLength(128);

                entity.Property(e => e.StorePhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ward).HasMaxLength(200);

                entity.Property(e => e.WardCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MOutletStoreUser>(entity =>
            {
                entity.ToTable("M_OutletStore_User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Area).HasMaxLength(400);

                entity.Property(e => e.AvatarUrl).HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.District).HasMaxLength(200);

                entity.Property(e => e.DistrictCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DrugStoreAddress).HasMaxLength(500);

                entity.Property(e => e.DrugStoreName).HasMaxLength(150);

                entity.Property(e => e.Latitue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longtitue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MediGroupCode).HasMaxLength(128);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Province).HasMaxLength(200);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SizeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StandardLevelCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreOwner).HasMaxLength(128);

                entity.Property(e => e.StorePhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(128);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ward).HasMaxLength(200);

                entity.Property(e => e.WardCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MProject>(entity =>
            {
                entity.ToTable("M_Project");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ParentProjectTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<MProjectDetail>(entity =>
            {
                entity.ToTable("M_ProjectDetail");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Area).HasMaxLength(250);

                entity.Property(e => e.Component)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CostPayForDrugStore).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CostPayForProduction).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DateBeginRent).HasColumnType("datetime");

                entity.Property(e => e.District).HasMaxLength(250);

                entity.Property(e => e.DrugName).HasMaxLength(250);

                entity.Property(e => e.Hsize).HasColumnName("HSize");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MediGroupCode).HasMaxLength(128);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Province).HasMaxLength(250);

                entity.Property(e => e.StoreOwner).HasMaxLength(250);

                entity.Property(e => e.StorePhoneNumber).HasMaxLength(250);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Vsize).HasColumnName("VSize");

                entity.Property(e => e.Ward).HasMaxLength(250);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.MProjectDetail)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_ProjectDetail_M_ProjectDetail");
            });

            modelBuilder.Entity<NewsPost>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CreatedById)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FullContent).IsRequired();

                entity.Property(e => e.MetaKeywords).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PublishedOn).HasColumnType("datetime");

                entity.Property(e => e.ShortContent).IsRequired();

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedById)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.NewsPost)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsPost_AspNetUsers");
            });

            modelBuilder.Entity<NewsPostCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.NewsPostId });
            });

            modelBuilder.Entity<PaymentContract>(entity =>
            {
                entity.Property(e => e.DatePayment).HasColumnType("datetime");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PaymentPeriod)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.TotalMoney).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.PaymentContract)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentContract_Contract");
            });

            modelBuilder.Entity<RegisterDateOff>(entity =>
            {
                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FromDateOff).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.Property(e => e.Refuse).HasMaxLength(450);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.ToDateOff).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(450);

                entity.Property(e => e.Type).HasMaxLength(500);

                entity.Property(e => e.UserApproved).HasMaxLength(450);

                entity.Property(e => e.UserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.UserApprovedNavigation)
                    .WithMany(p => p.RegisterDateOffUserApprovedNavigation)
                    .HasForeignKey(d => d.UserApproved)
                    .HasConstraintName("FK_RegisterDateOff_AspNetUsers");

                entity.HasOne(d => d.UserCreateNavigation)
                    .WithMany(p => p.RegisterDateOffUserCreateNavigation)
                    .HasForeignKey(d => d.UserCreate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterDateOff_RegisterDateOff");
            });
        }
    }
}
