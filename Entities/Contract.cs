using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class Contract
    {
        public Contract()
        {
            PaymentContract = new HashSet<PaymentContract>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? CustomerId { get; set; }
        public string UserCreated { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Files { get; set; }
        public string Status { get; set; }
        public string NoContract { get; set; }
        public decimal TotalMoney { get; set; }

        public Customer Customer { get; set; }
        public AspNetUsers UserCreatedNavigation { get; set; }
        public ICollection<PaymentContract> PaymentContract { get; set; }
    }
}
