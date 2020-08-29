using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class ProductApiModel
    {
        public EProduct prodcutModel { get; set; }
        public List<RelatedProduct> arrayProduct { get; set; }
    }

    public class RelatedProduct
    {
        public long Id  { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
    }
}
