using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class Category
    {
        public Category()
        {
            Files = new HashSet<Files>();
            InverseCategoryNavigation = new HashSet<Category>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
        public bool ModerateTopics { get; set; }
        public bool ModeratePosts { get; set; }
        public DateTime DateCreated { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public string MetaDescription { get; set; }
        public string Image { get; set; }
        public string SeoTitle { get; set; }
        public Guid? CategoryId { get; set; }

        public Category CategoryNavigation { get; set; }
        public ICollection<Files> Files { get; set; }
        public ICollection<Category> InverseCategoryNavigation { get; set; }
    }
}
