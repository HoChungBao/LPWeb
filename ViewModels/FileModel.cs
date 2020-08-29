using LienPhatERP.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class FileModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ICollection<IFormFile> Files { get; set; }
        [Required]
        public bool? IsPublic { get; set; }
    }
    public class FileServiceModel
    {
        public List<Category> Category { get; set; }
        public List<Files> File { get; set; }
        public FileServiceModel()
        {
            Category = new List<Category>();
            File = new List<Files>();
        }
    }
}
