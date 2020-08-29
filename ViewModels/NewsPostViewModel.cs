using LienPhatERP.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public partial class NewsPostViewModel
    {
        public NewsPostViewModel()
        {
            //IsPublished = true;
        }
        public long Id { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

        [Required]
        public string MetaKeywords { get; set; }

        [Required]
        public string Name { get; set; }
        
        public DateTime CreatedOn { get; set; }

        [Required]
        public string FullContent { get; set; }
        
        private bool isDeleted { get; set; }
        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }

            set
            {
                isDeleted = value;
               
            }
        }

        public bool IsPublished
        {
            get; set;
        }
        public bool IsSticky
        {
            get; set;
        }
        public DateTime? PublishedOn { get; set; }

        //[Required]
        public string SeoTitle { get; set; }

        [Required]
        public string ShortContent { get; set; }

        public string LinkImage { get; set; }

        //public string Category { get; set; }

        //public List<CategotyViewModel> Categories { get; set; }

        public IFormFile FileImage { get; set; }

        public string CreatedById { get; set; }
        
        public long? ImageId { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        //[Required]
        public string Slug { get; set; }
        public long? CategoryId { get; set; }
    }

    public class EditNewsPost
    {
        public NewsPost newsPost { get; set; }
        public List<Category> categories { get; set; }
    }

}
