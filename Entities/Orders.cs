using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string VideoLink { get; set; }
        public string RelatedDocuments { get; set; }
        public string Status { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalSubMoney { get; set; }
        public string DiscountCode { get; set; }
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? OfficalStartDate { get; set; }
        public DateTime OfficalEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
