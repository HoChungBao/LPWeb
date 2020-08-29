using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class MediHubAsset
    {
        public MediHubAsset()
        {
            DsPlayer = new HashSet<DsPlayer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Specicality { get; set; }
        public long CsScreenId { get; set; }
        public string Address { get; set; }
        public string Tvbrand { get; set; }
        public string Tvsize { get; set; }
        public string Seri { get; set; }
        public string Box { get; set; }
        public string Timer { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserUpdated { get; set; }
        public string ContractType { get; set; }

        public AspNetUsers UserUpdatedNavigation { get; set; }
        public ICollection<DsPlayer> DsPlayer { get; set; }
    }
}
