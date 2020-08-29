using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class AirContents
    {
        public AirContents()
        {
            AirContentDetail = new HashSet<AirContentDetail>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Files { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int Durations { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }

        public ICollection<AirContentDetail> AirContentDetail { get; set; }
    }
}
