using LienPhatERP.Entities;
using System.Collections.Generic;

namespace LienPhatERP.Services
{
    public interface INewsService
    {
        Category InsertCategory(Category entity);
        List<NewsPost> GetDataJsonNewsPost();
        NewsPost GetNewsPostsBySlug(string slug);
        NewsPost GetNewsById(long id);
        List<Category> GetCategories();
        NewsPost UpdateNewsPost(NewsPost entity);
        List<Category> GetCategory();
        NewsPost InsertNewsPost(NewsPost entity);
        List<NewsPost> GetDataNewsPosts();
        bool DeteltePost(long id);
        List<NewsPost> GetListTopPosts(int pageSize);
    }
}