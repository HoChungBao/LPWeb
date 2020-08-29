using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class CsCreens
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Speciality { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public int TotalLed { get; set; }
        public int TrafficDaily { get; set; }
        public bool IsMonopoly { get; set; }
        public decimal PriceWeekly { get; set; }
        public decimal PriceMonopoly { get; set; }
        public string AdvertiseProductsType { get; set; }
        public bool IsHospital { get; set; }
        public double Lat { get; set; }
        public double Ln { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Images { get; set; }
        public int NumberApprovedDay { get; set; }
        public string DescriptionPoint { get; set; }
        public string LinkProfile { get; set; }
        public string AmountScreenDetails { get; set; }
        public int? AmountWifi { get; set; }
    }
}
