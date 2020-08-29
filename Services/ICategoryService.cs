using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public interface ICategoryService
    {
        Category GetId(Guid id);
        Category Get(string name);
        List<Category> GetAll();
        Category GetCategoryBySlug(string slug);
        List<Category> GetAllSubCategory(Guid idParent);
        Category Add(Category category);
        IList<Category> GetBySlugLike(string slug);
        List<Category> GetCategorie();
        Category Update(Category category);
        Category UpdateName(Category category);
    }
}
