using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class MProjectCustomerModel
    {
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
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
    }
}
