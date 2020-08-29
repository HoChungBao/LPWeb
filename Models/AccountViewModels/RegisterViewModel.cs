using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Is Manager")]
        public bool IsManager { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; }
        //[Required]
        public string RoleId { get; set; }
        public string IdentityCard { get; set; }
        public string IssuedBy { get; set; }
        public string IssuedDate { get; set; }
        public string Level { get; set; }
        public string GraduationYear { get; set; }
        public string Male { get; set; }
        public string School { get; set; }
        public string Folk { get; set; }
    }
    public class RegisterModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "User Name")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DateWork { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string Files { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public string Home { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityCard { get; set; }
        public string IssuedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? IssuedDate { get; set; }
        public string Level { get; set; }
        public string GraduationYear { get; set; }
        public string Male { get; set; }
        public string School { get; set; }
        public string Folk { get; set; }
        public IFormFile File { get; set; }
        public string NoContract { get; set; }
        public string NoBHXH { get; set; }
        public string CurrentContract { get; set; }
        public string UserCode { get; set; }
        public string ContractType { get; set; }
        public string TGHD { get; set; }
        public string TGBH { get; set; }
        public string Bank { get; set; }
        public string Matrimony { get; set; }
        public decimal? Salary { get; set; }
        public RegisterModel() { } 
        public RegisterModel(ApplicationUser model)
        {
            Id = model.Id;
            UserName = model.UserName;
            Email = model.Email;
            Name = model.Name;
            DateWork = model.DateWork;
            Image = model.Image;
            Files = model.Files;
            Date = model.Date;
            Home = model.Home;
            Address = model.Address;
            Province = model.Province;
            District = model.District;
            CompanyName = model.CompanyName;
            PhoneNumber = model.PhoneNumber;
            IdentityCard = model.IdentityCard;
            IssuedBy = model.IssuedBy;
            IssuedDate = model.IssuedDate;
            Level = model.Level;
            GraduationYear = model.GraduationYear;
            Male = model.Male;
            School = model.School;
            Folk = model.Folk;
            Position = model.Position;
            NoContract = model.NoContract;
            NoBHXH = model.NoBHXH;
            CurrentContract = model.CurrentContract;
            UserCode = model.UserCode;
            ContractType = model.ContractType;
            TGHD = model.TGHD;
            TGBH = model.TGBH;
            Bank = model.Bank;
            Matrimony = model.Matrimony;
            Salary =model.Salary;
        }
    }
    public class ChangeImageModel
    {
        public string Id { get; set; }

        //  [Required]
        public IFormFile File { get; set; }

    }
}
