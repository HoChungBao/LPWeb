using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class NewsPost
    {
        public long Id { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Name { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FullContent { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string SeoTitle { get; set; }
        public string ShortContent { get; set; }
        public long? ImageId { get; set; }
        public string LinkImage { get; set; }
        public string UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Slug { get; set; }
        public long? CategoryId { get; set; }
        public string BannerLink { get; set; }
        public bool IsSticky { get; set; }

        public AspNetUsers CreatedBy { get; set; }
    }
}
