using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class ProvinceViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<DistrictViewModel> Districts { get; set; }

    }
    public class DistrictViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<ItemCombobox> Wards { get; set; }

    }
    public class ItemCombobox
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
    public class ProvinceViewModelCode
    {
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
    }
}
