using LienPhatERP.Entities;
using LienPhatERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class MOutletStoreModel
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

        public void Update(MOutletStore rs)
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
            if ((!string.IsNullOrEmpty(Ward) && !Ward.Equals(rs.Ward)) || (!string.IsNullOrEmpty(District) && !District.Equals(rs.District)) || (!string.IsNullOrEmpty(Province) && !Province.Equals(rs.Province)))
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
    public class MOutletStoreProjectDetailModel
    {
        public MOutletStoreProjectDetailModel()
        {
            ProjectDetail = new HashSet<ProjectDetailModel>();
        }
        public MOutletStoreProjectDetailModel(MOutletStore model)
        {
            Id = model.Id;
            District = model.District;
            Province = model.Province;
            Area = model.Area;
            SizeCode = model.SizeCode;
            SizeCode = model.SizeCode;
            MediGroupCode = model.MediGroupCode;
            DrugStoreName = model.DrugStoreName;
            DrugStoreAddress = model.DrugStoreAddress;
            CreatedBy = model.CreatedBy;
            CreatedDate = model.CreatedDate;
            Status = model.Status;
            StorePhoneNumber = model.StorePhoneNumber;
            StandardLevelCode = model.StandardLevelCode;
            StaffNumm = model.StaffNumm;
            Note = model.Note;
            AvatarUrl = model.AvatarUrl;
            UnitCost = model.UnitCost;
            Ward = model.Ward;
            Type = model.Type;
        }
        public MOutletStoreProjectDetailModel(MOutletStoreUser model)
        {
            Id = model.Id;
            District = model.District;
            Province = model.Province;
            Area = model.Area;
            SizeCode = model.SizeCode;
            SizeCode = model.SizeCode;
            MediGroupCode = model.MediGroupCode;
            DrugStoreName = model.DrugStoreName;
            DrugStoreAddress = model.DrugStoreAddress;
            CreatedBy = model.CreatedBy;
            CreatedDate = model.CreatedDate;
            Status = model.Status;
            StorePhoneNumber = model.StorePhoneNumber;
            StandardLevelCode = model.StandardLevelCode;
            StaffNumm = model.StaffNumm;
            Note = model.Note;
            AvatarUrl = model.AvatarUrl;
            UnitCost = model.UnitCost;
            Ward = model.Ward;
            Type = model.Type;
            UpdatedBy = model.UpdatedBy;
            UpdatedDate = model.UpdatedDate;
            Updated = model.Updated;
        }
        public long Id { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string SizeCode { get; set; }
        public string MediGroupCode { get; set; }
        public string DrugStoreName { get; set; }
        public string DrugStoreAddress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StoreOwner { get; set; }
        public string BankAccount { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
        public string StorePhoneNumber { get; set; }
        public string StandardLevelCode { get; set; }
        public int? StaffNumm { get; set; }
        public string Note { get; set; }
        public string AvatarUrl { get; set; }
        public decimal? UnitCost { get; set; }
        public string Ward { get; set; }
        public string Type { get; set; }
        public bool? Updated { get; set; }
        public ICollection<ProjectDetailModel> ProjectDetail { get; set; }
    }
    public class ProjectDetailModel
    {
        public ProjectDetailModel()
        {

        }
        public ProjectDetailModel(MProjectDetail model)
        {
            Id = model.Id;
            Component = model.Component;
            Label = model.Label;
            Position = model.Position;
            Hsize = model.Hsize;
            Vsize = model.Vsize;
            CostPayForDrugStore = model.CostPayForDrugStore;
            CostPayForProduction = model.CostPayForProduction;
            DateBeginRent = model.DateBeginRent;
            MonthRent = model.MonthRent;
            ProjectId = model.ProjectId;
            District = model.District;
            Province = model.Province;
            Area = model.Area;
            Address = model.Address;
            Ward = model.Ward;
            ProjectName = model.Project.Name;
            CustomerId = model.Project.CustomerId;
            MediGroupCode = model.MediGroupCode;
            Image = model.Images;
        }
        public long Id { get; set; }
        public string Component { get; set; }
        public string Label { get; set; }
        public string Position { get; set; }
        public double Hsize { get; set; }
        public double Vsize { get; set; }
        public double NumOfArea { get; set; }
        public decimal CostPayForDrugStore { get; set; }
        public decimal CostPayForProduction { get; set; }
        public DateTime DateBeginRent { get; set; }
        public int MonthRent { get; set; }
        public int ProjectId { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string ProjectName { get; set; }
        public int CustomerId { get; set; }
        public string MediGroupCode { get; set; }
        public string Image { get; set; }
    }
    public class MOutletStoreApiModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string Latitue { get; set; }
        public string Longtitue { get; set; }
    }
}
