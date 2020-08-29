using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class AirContentSearch
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public int TotalLed { get; set; }
        public int TrafficDaily { get; set; }
        public int RepeatTimes { get; set; }
        public int FreeTime { get; set; }
    }
}
