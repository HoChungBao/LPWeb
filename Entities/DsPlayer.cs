using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class DsPlayer
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
        public bool? IsDelete { get; set; }
        public MediHubAsset MedihubAsset { get; set; }
        public AspNetUsers User { get; set; }
    }
}
