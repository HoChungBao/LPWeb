using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Contact { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Pic { get; set; }
        public string Position { get; set; }
        public string UserPic { get; set; }
        public bool IsAgency { get; set; }

        public AspNetUsers UserPicNavigation { get; set; }
        public ICollection<Contract> Contract { get; set; }
    }
}
