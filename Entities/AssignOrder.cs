using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class AssignOrder
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Status { get; set; }
    }
}
