using LienPhatERP.Entities;
using LienPhatERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class MOutletStoreUserModel
    {
        public long Id { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string DistrictCode { get; set; }
        public string ProvinceCode { get; set; }
        public string SizeCode { get; set; }
        public string Latitue { get; set; }
        public string Longtitue { get; set; }
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

        public void Update(MOutletStoreUser rs)
        {
            rs.District = StringUtils.RemoveVietNam(District).ToUpper();
            rs.Province = StringUtils.RemoveVietNam(Province).ToUpper();
            rs.Area = Area;
            rs.DistrictCode = DistrictCode;
            rs.ProvinceCode = ProvinceCode;
            rs.SizeCode = SizeCode;
            rs.DrugStoreName = DrugStoreName;
            rs.DrugStoreAddress = DrugStoreAddress;
            rs.StoreOwner = StoreOwner;
            rs.BankAccount = BankAccount;
            rs.Status = Status;
            rs.StorePhoneNumber = StorePhoneNumber;
            rs.StandardLevelCode = StandardLevelCode;
            rs.StaffNumm = StaffNumm;
            rs.Note = Note;
            rs.UnitCost = UnitCost;
            rs.Ward = StringUtils.RemoveVietNam(Ward).ToUpper();
            rs.WardCode = WardCode;
            rs.Type = Type;
            if (!string.IsNullOrEmpty(AvatarUrl))
            {
                rs.AvatarUrl = AvatarUrl;
            }
            if((!string.IsNullOrEmpty(Ward) && !Ward.Equals(rs.Ward)) || (!string.IsNullOrEmpty(District) && !District.Equals(rs.District)) || (!string.IsNullOrEmpty(Province) && !Province.Equals(rs.Province)))
            {
                var location = StaticHelper.GetLatLong($"{DrugStoreName} {Ward} {District} {Province}");
                if (location != null)
                {
                    Latitue = location.Latitude.ToString();
                    Longtitue = location.Longitude.ToString();
                }
            }
        }
    }
}
