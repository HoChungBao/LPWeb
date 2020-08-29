using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class EmaiSended
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ForId { get; set; }
        public string Type { get; set; }
    }
}
