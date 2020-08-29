using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class DsPlayerLogModel
    {
        public long Id { get; set; }
        public string PlayerId { get; set; }
        public string Status { get; set; }
        public string ImageLink { get; set; }
        public string Note { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateApprove { get; set; }
        public string UserId { get; set; }
        public string PlayerName { get; set; }
        public string Address { get; set; }
        public string Require { get; set; }

        public AspNetUsers User { get; set; }
    }
}
