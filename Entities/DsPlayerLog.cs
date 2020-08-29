using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class DsPlayerLog
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
        public string Require { get; set; }
        public string Address { get; set; }

        public AspNetUsers User { get; set; }
    }
}
