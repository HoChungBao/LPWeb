using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Entities;

namespace LienPhatERP.ViewModels
{
    public partial class OrderModel
    {
        public OrderModel()
        {
            CreatedDate =  DateTime.Now;
            Status = OrderStatus.New.ToString();
            OrderDetails = new List<OrderDetails>();
        }
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string VideoLink { get; set; }
        public string RelatedDocuments { get; set; }
        public List<long> CsCreensHospital { get; set; }
        public List<long> CsCreensPharmacy { get; set; }
        public string Status { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalSubMoney { get; set; }
        public string DiscountCode { get; set; }
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime OfficalStartDate { get; set; }
        public DateTime OfficalEndDate { get; set; }
        public string SpecicalyAboutProduct { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool AllHospital { get; set; }
        public long Weekly { get; set; }
        public long Time { get; set; }
        public bool LicenseProduct { get; set; }
        //public List<ProductTypeViewModel> ListProductType { get; set; }
        public string StrDateExpectedStartDate { get; set; }
        public string StrDateExpectedEndDate { get; set; }
        public string FullAddress { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public List<ProductTypeViewModel> ProductTypes { get; set; }
        public void CaculatorPrice()
        {
            OfficalStartDate = ExpectedStartDate;
            OfficalEndDate = ExpectedEndDate;
            TotalMoney = OrderDetails.Select(x => x.Price * x.Amount).Sum();
            TotalSubMoney = TotalMoney - Discount;
        }
    }

    public class RelatedDocument
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }


}
