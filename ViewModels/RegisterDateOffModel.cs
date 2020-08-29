using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class RegisterDateOffModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string UserCreate { get; set; }
        public string UserApproved { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        public double DateOffNumber { get; set; }
        public DateTime FromDateOff { get { return DateTime.ParseExact(StrFromDateOff, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public DateTime ToDateOff { get { return DateTime.ParseExact(StrToDateOff?? StrFromDateOff, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public string StrFromDateOff { get; set; }
        public string StrToDateOff { get; set; }
        public string Department { get; set; }
        public string Token { get; set; }
        public string Refuse { get; set; }
        public AspNetUsers UserApprovedNavigation { get; set; }
        public AspNetUsers UserCreateNavigation { get; set; }
    }
}
