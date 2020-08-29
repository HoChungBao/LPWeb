using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class OrderDetails
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long CsCreensId { get; set; }
        public string TypeCreen { get; set; }
        public string CsCreensName { get; set; }
        public DateTime ApprovedDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public bool IsDuyet { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }

        public Orders Order { get; set; }
    }
}
