using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class EContactForm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public long? ProductId { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }

        public EProduct Product { get; set; }
    }
}
