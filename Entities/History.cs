using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class History
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
        public AspNetUsers UserUpdateNavigation { get; set; }
    }
}
