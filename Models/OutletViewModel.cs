using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;

namespace LienPhatERP.Models
{
    public class OutletViewModel
    {
        public MOutletStore Outlet { get; set; }
        public List<MProjectDetail> Projects { get; set; }
    }
}
