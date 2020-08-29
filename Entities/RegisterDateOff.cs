using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class RegisterDateOff
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string UserCreate { get; set; }
        public string UserApproved { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }
        public double DateOffNumber { get; set; }
        public DateTime FromDateOff { get; set; }
        public DateTime ToDateOff { get; set; }
        public string Token { get; set; }
        public string Refuse { get; set; }

        public AspNetUsers UserApprovedNavigation { get; set; }
        public AspNetUsers UserCreateNavigation { get; set; }
    }
}
