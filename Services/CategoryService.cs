using LienPhatERP.Entities;
using LienPhatERP.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LienPhatERP.Services
{
    public class CategoryService : ICategoryService
    {
        private MediGroupContext _context;
        private IMemoryCache _cache;

        public CategoryService(MediGroupContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public Category Get(string name)
        {
            return _context.Category.FirstOrDefault(x => x.Name == name);
        }

        public List<Category> GetAll()
        {
            var data = _cache.Get<List<Category>>($"GetAll");
            if (data != null)
            {
                return data;
            }
            data = _context.Category
                .AsNoTracking()
                .ToList();
            _cache.Set($"GetAll", data, TimeSpan.FromMinutes(1));
            return data;
        }
        public List<Category> GetCategorie()
        {
            var data = _cache.Get<List<Category>>($"GetCategorie");
            if (data != null)
            {
                return data;
            }
            data = _context.Category
                .AsNoTracking()
                .Where(x=> x.CategoryId == null|| x.CategoryId == Guid.Empty)
                .ToList();
            _cache.Set($"GetCategorie", data, TimeSpan.FromMinutes(1));
            return data;
        }
        public List<Category> GetAllSubCategory(Guid idParent)
        {
            return _context.Category.Where(x => x.CategoryId == idParent).ToList();
        }

        public Category GetCategoryBySlug(string slug)
        {
            return _context.Category.FirstOrDefault(x => x.Slug.ToLower().Equals(slug.ToLower()));
        }

        public Category GetId(Guid id)
        {
            return _context.Category.FirstOrDefault(x => x.Id == id);
        }

        public Category Add(Category category)
        {
            category.Id = Guid.NewGuid();
            // Sanitize
            category = SanitizeCategory(category);

            // Set the create date
            category.DateCreated = DateTime.UtcNow;

            // url slug generator
            category.Slug = ServiceHelpers.GenerateSlug(category.Name, GetBySlugLike(ServiceHelpers.CreateUrl(category.Name)), null);

            category.IsDeleted = false;
            category.IsLocked= false;
            category.IsPublished = false;
            // Add the category

            _context.Category.Add(category);

            _context.SaveChanges();
            return category;
        }
        public Category UpdateName(Category category)
        {
            // url slug generator
            category.Slug = ServiceHelpers.GenerateSlug(category.Name, GetBySlugLike(ServiceHelpers.CreateUrl(category.Name)), null);

            _context.Category.Update(category);

            _context.SaveChanges();
            return category;
        }
        public Category Update(Category category)
        {
            _context.Category.Update(category);

            _context.SaveChanges();
            return category;
        }
        public Category SanitizeCategory(Category category)
        {
            // Sanitize any strings in a category
            category.Description = StringUtils.GetSafeHtml(category.Description);
            category.Name = HttpUtility.HtmlDecode(StringUtils.SafePlainText(category.Name));
            return category;
        }

        public IList<Category> GetBySlugLike(string slug)
        {
            return _context.Category
                    .Where(x => x.Slug.Contains(slug))
                    .ToList();
        }
    }
}
