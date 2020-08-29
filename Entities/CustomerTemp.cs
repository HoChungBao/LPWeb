using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class CustomerTemp
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
