using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class CategotyViewModel
    {
        public CategotyViewModel()
        {
            IsPublished = true;
        }
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public bool ModerateTopics { get; set; }

        public bool ModeratePosts { get; set; }

        public bool IsMediHubSc { get; set; }

        //[Required]
        public string Slug { get; set; }

        public string SeoTitle { get; set; }

        //[Required]
        public bool IsPublished { get; set; }

        public string MetaDescription { get; set; }

        public string Image { get; set; }

        public string Path { get; set; }

        public DateTime DateCreated { get; set; }

        // public List<CategotyViewModel> Categories { get; set; }
    }
}
