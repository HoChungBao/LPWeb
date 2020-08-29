using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class PostCategoryViewModel
    {
        public PostCategoryViewModel()
        {
            Categoty = new List<CategotyViewModel>();
        }
        public CategotyViewModel CurrentCat { get; set; }

        public IList<CategotyViewModel> Categoty { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItem { get; set; }
    }
}
