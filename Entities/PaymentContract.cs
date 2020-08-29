using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class PaymentContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal TotalMoney { get; set; }
        public string PaymentPeriod { get; set; }
        public DateTime DatePayment { get; set; }
        public DateTime DateUpdate { get; set; }
        public long ContractId { get; set; }
        public string Status { get; set; }

        public Contract Contract { get; set; }
    }
}
