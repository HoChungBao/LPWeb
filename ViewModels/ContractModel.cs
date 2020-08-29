using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class ContractModel
    {
        public ContractModel()
        {
            PaymentContract = new HashSet<PaymentContract>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? CustomerId { get; set; }
        public string UserCreated { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get { return DateTime.ParseExact(StrStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public DateTime? EndDate { get { return DateTime.ParseExact(StrEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public string Files { get; set; }
        public string Status { get; set; }
        public string NoContract { get; set; }
        public decimal TotalMoney { get; set; }
        public string StrStartDate { get; set; }
        public string StrEndDate { get; set; }
        public string paymentContract { get; set; }

        public Customer Customer { get; set; }
        public AspNetUsers UserCreatedNavigation { get; set; }
        public ICollection<PaymentContract> PaymentContract { get; set; }
    }
    public class ContactApiModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email không hợp lệ")]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
