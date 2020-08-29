using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LienPhatERP.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsManager { get; set; }
        public string Name { get; set; }
        public DateTime? DateWork  { get; set; }
        public string Image { get; set; }
        public string Files { get; set; }
        public DateTime? Date {get; set; }
        public string Home { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string CompanyName { get; set; }
        public string IdentityCard { get; set; }
        public string IssuedBy{ get; set; }
        public DateTime? IssuedDate { get; set; }      
        public string Level { get; set; }
        public string GraduationYear { get; set; }
        public string Male { get; set; }
        public string School { get; set; }
        public string Folk { get; set; }
        public string NoContract { get; set; }
        public string NoBHXH { get; set; }
        public string CurrentContract { get; set; }
        public string Position { get; set; }
        public string UserCode { get; set; }
        public string ContractType { get; set; }
        public string TGHD { get; set; }
        public string TGBH { get; set; }
        public string Bank { get; set; }
        public string Matrimony { get; set; }
        public decimal? Salary { get; set; }
    }
   
}
