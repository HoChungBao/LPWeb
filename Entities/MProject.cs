using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class MProject
    {
        public MProject()
        {
            MProjectDetail = new HashSet<MProjectDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectType { get; set; }
        public int CustomerId { get; set; }
        public int? ParentProjectId { get; set; }
        public string ParentProjectTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public string Slug { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Value { get; set; }

        public ICollection<MProjectDetail> MProjectDetail { get; set; }
    }
}
