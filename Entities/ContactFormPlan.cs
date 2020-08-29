using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class ContactFormPlan
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
       
        public string Note { get; set; }
      
        public DateTime DateCreated { get; set; }
    }
}
