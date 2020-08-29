using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class DsPlayerModel
    {
        public long Id { get; set; }
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public bool? IsOffline { get; set; }
        public string Note { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CurrentStatus { get; set; }
        public bool? IsHospital { get; set; }
        public string Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public long? MedihubAssetId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsDelete { get; set; }
        public MediHubAsset MedihubAsset { get; set; }
        public AspNetUsers User { get; set; }
        public void UpdateDsPlayer(DsPlayer rs)
        {
            rs.PlayerId = PlayerId;
            rs.PlayerName = PlayerName;
            rs.Address = Address;
            rs.Province = Province;
            rs.District = District;
            rs.IsOffline = IsOffline;
            rs.Note = Note;
            //rs.DateCreated = DateCreated;
            //rs.CurrentStatus = CurrentStatus;
            rs.IsHospital = IsHospital;
            //rs.Status = Status;
            //rs.DateUpdated = DateUpdated;
            //rs.MedihubAssetId = MedihubAssetId;
            //rs.UserId = UserId;
            //rs.UserName = UserName;
        }
    }
}
