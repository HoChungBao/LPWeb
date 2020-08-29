using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class AirContentDetail
    {
        public long Id { get; set; }
        public long CsScreenId { get; set; }
        public string Status { get; set; }
        public long AirId { get; set; }

        public AirContents Air { get; set; }
        public CsCreens CsScreen { get; set; }
    }
}
