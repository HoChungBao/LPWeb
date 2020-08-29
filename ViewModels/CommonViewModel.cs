using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;

namespace LienPhatERP.ViewModels
{
    public class CommonViewModel
    {
        public List<string> SpeciaList { get; set; }
        public List<ProductTypeViewModel> ProductTypes { get; set; }
        public List<CsCreens> HospitalName { get; set; }
        public List<ProvinceViewModel> Province { get; set; }

    }
}
