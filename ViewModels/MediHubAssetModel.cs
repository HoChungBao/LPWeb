using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class MediHubAssetModel
    {
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
        public DateTime CreatedDate { get; set; }
        public string UserUpdated { get; set; }
        public string ContractType { get; set; }
        public string Status { get; set; }
        public AspNetUsers UserUpdatedNavigation { get; set; }
        public ICollection<DsPlayer> DsPlayer { get; set; }
        public void Update(MediHubAsset rs)
        {
            rs.Name = Name;
            rs.Address = Address;
            rs.Box = Box;
            rs.Seri = Seri;
            rs.Tvbrand = Tvbrand;
            rs.Tvsize = Tvsize;
            rs.Specicality = Specicality;
            rs.Timer = Timer;
            rs.Note = Note;
            rs.CsScreenId = CsScreenId;
            rs.ContractType = ContractType;
        }
    }
}
