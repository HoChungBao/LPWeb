using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class MDrugLabel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Slug { get; set; }
    }
}
