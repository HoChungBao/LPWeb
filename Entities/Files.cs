using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class Files
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool? IsPublic { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? IsPrioritize { get; set; }
        public string Type { get; set; }

        public Category Category { get; set; }
        public AspNetUsers User { get; set; }
    }
}
