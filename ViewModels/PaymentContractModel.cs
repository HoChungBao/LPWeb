using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class PaymentContractModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal TotalMoney { get; set; }
        public string PaymentPeriod { get; set; }
        public DateTime DatePayment {
            get { return DateTime.ParseExact(StrDatePayment, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public string StrDatePayment { get; set; }
        public DateTime DateUpdate { get; set; }
        public long ContractId { get; set; }
        public string Status { get; set; }
        public Contract Contract { get; set; }
    }
}
