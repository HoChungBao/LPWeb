using LienPhatERP.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class FileViewModel
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
        public IFormFile File { get; set; }

        public Category Category { get; set; }
        public AspNetUsers User { get; set; }
    }
}
