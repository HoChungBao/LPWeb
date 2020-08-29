using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class MOutletStore
    {
        public long Id { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string DistrictCode { get; set; }
        public string ProvinceCode { get; set; }
        public string SizeCode { get; set; }
        public string Latitue { get; set; } = "0";
        public string Longtitue { get; set; } = "0";
        public string MediGroupCode { get; set; }
        public string DrugStoreName { get; set; }
        public string DrugStoreAddress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StoreOwner { get; set; }
        public string BankAccount { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; }
        public string StorePhoneNumber { get; set; }
        public string StandardLevelCode { get; set; }
        public int? StaffNumm { get; set; }
        public string Note { get; set; }
        public string AvatarUrl { get; set; }
        public decimal UnitCost { get; set; }
        public string Ward { get; set; }
        public string WardCode { get; set; }
        public string Type { get; set; }
    }
}
