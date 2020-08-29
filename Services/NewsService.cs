using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LienPhatERP.Services
{
    public class NewsService : INewsService
    {
        private readonly MediGroupContext _context;
        private readonly IMemoryCache _cache;

        public NewsService(MediGroupContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        #region News Post
        public Category InsertCategory(Category entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.Slug = convertToUnSign3(entity.Name.Replace(" ","-"));
            entity.IsPublished = true;
            entity.IsDeleted = false;
            entity.IsDeleted = false;
            entity.IsLocked = false;
            //entity.IsMediHubSc = false;

            var item = _context.Category.Add(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public List<NewsPost> GetDataJsonNewsPost()
        {
            return _context.NewsPost.OrderByDescending(x => x.CreatedOn).ToList();
        }

        public List<NewsPost> GetDataNewsPosts()
        {
            return _context.NewsPost.OrderByDescending(x => x.CreatedOn).ToList();
        }

        //public NewsPost GetNewsPostsBySlug(string slug)
        //{
        //    //slug = StringUtils.GetSafeHtml(slug);
        //    return _context.NewsPost.Include(x=>x.CreatedBy).FirstOrDefault(x => x.Slug.Equals(slug));
        //}

        public NewsPost GetNewsById(long id)
        {
            return _context.NewsPost.FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetCategories()
        {
            List<Category> cacheCategories = null;
            if (_cache.TryGetValue("GetCategories", out cacheCategories))
            {
                return cacheCategories;
            }
            else
            {
                var item = _context.Category.ToList();
                if (item.Count > 0)
                {
                    _cache.Set("GetCategories", item, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
                }

                return item;
            }

        }

        public NewsPost UpdateNewsPost(NewsPost entity)
        {
            entity.CreatedOn = DateTime.Now;

            entity.Slug = StringUtils.CreateUrl(entity.Name);
            entity.UpdatedOn = DateTime.Now;
            //if (entity.IsPublished == null)
            //{
            //    entity.IsPublished = "Chờ duyệt";
            //}
            //entity.CategoryId
            if (string.IsNullOrEmpty(entity.LinkImage))
            {
                entity.LinkImage = "{\"Url\":\" \"}";
            }
            var item = _context.NewsPost.Update(entity);
            _context.SaveChanges();
            return item.Entity;
        }
        #endregion

        #region Post

        public List<Category> GetCategory()
        {
            List<Category> cacheCategories = null;
            if (_cache.TryGetValue("GetCategory", out cacheCategories))
            {
                return cacheCategories;
            }
            else
            {
                var item = _context.Category.ToList();
                if (item.Count > 0)
                {
                    _cache.Set("GetCategory", item, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
                }

                return item;
            }
            //var item = _context.Category.ToList();
            //return item;
            //return _context.Category.OrderByDescending(x => x.DateCreated).Take(100).ToList();
        }

        public NewsPost InsertNewsPost(NewsPost entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.Slug = StringUtils.CreateUrl(entity.Name);
            entity.UpdatedOn = DateTime.Now;
            //entity.CategoryId
            //if (entity.IsPublished == null)
            //{
            //    entity.IsPublished = "Chờ duyệt";
            //}
            if (string.IsNullOrEmpty(entity.LinkImage))
            {
                entity.LinkImage = "{\"Url\":\" \"}";
            }
            if (string.IsNullOrEmpty(entity.MetaKeywords))
            {
                entity.MetaKeywords = "MediHub, Digital Signage";
            }
            var item = _context.NewsPost.Add(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public bool DeteltePost(long id)
        {
            var rs = _context.NewsPost.FirstOrDefault(x => x.Id == id);
            if (rs == null) return false;
            _context.NewsPost.Remove(rs);
            _context.SaveChanges();
            return true;
        }

        public List<NewsPost> GetListTopPosts(int pageSize)
        {
            if (_cache.TryGetValue("GetListTopPosts", out List<NewsPost> cacheListTopPosts))
            {
                return cacheListTopPosts;
            }
            var item = _context.NewsPost.Take(pageSize).OrderByDescending(x => x.CreatedOn).ToList();
            if (item.Count > 0)
            {
                _cache.Set("GetListTopPosts", item, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
            }

            return item;
        }

        public NewsPost GetNewsPostsBySlug(string slug)
        {
            return _context.NewsPost.Include(x => x.CreatedBy).FirstOrDefault(x => x.Slug.Equals(slug));
        }
        //public NewsPost GetNewsDetails(string slug)
        //{
        //    return _context.NewsPost.FirstOrDefault(x => x.Id == 30);
        //}
        #endregion
    }
}
