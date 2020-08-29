using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;

namespace LienPhatERP.ViewModels
{
    public class DashboardCsViewModel
    {
        public List<DsPlayer> ListPlayerOff { get; set; }
        public int TotalNumOfBox { get; set; }
        public int NumOfBoxOn { get; set; }
        public int NumOfBoxOff { get; set; }
        public int MtdOffDevice { get; set; }
        public int NumOfOffDeviceBox { get; set; }
        public int NumOfOffDeviceSCreen { get; set; }
    }
}
