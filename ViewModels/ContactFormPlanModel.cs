using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class ContactFormPlanModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string VideoLink { get; set; }
        public decimal BudgetTotal { get; set; }
        public string Note { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class ContactFormProductModel
    {
        public ContactFormPlan contactFormPlan { get; set; }
        public List<ProductType> productType { get; set; }
    }
}
