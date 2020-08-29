using LienPhatERP.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public class FileService : IFileService
    {
        private MediGroupContext _context;
        private readonly IHostingEnvironment _env;
        public FileService(MediGroupContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public Files Delete(Files entity)
        {
            _context.Files.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public Files GetFile(Guid id)
        {
            return _context.Files.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<string> GetFileName(string path)
        {
            string webRootPath = _env.WebRootPath;
            string dirPath = Path.Combine(webRootPath, path);
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            return dirInfo.GetFiles().Select(x=>x.Name).ToList();
        }

        public Files Insert(Files entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;
            entity.IsPublic = true;
            entity.IsPrioritize = false;
            _context.Files.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Files Update(Files entity)
        {
            _context.Files.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public List<Files> GetAllFile()
        {
            return _context.Files.OrderByDescending(x=>x.DateCreated).ToList();
        }
        public List<Files> GetAllFileByType(Guid categoryId, string type)
        {
            return _context.Files.Where(x=>x.CategoryId== categoryId&& x.Type==type).OrderByDescending(x => x.DateCreated).ThenByDescending(x=>x.IsPrioritize).ToList();
        }
        public List<Files> GetAllFileByTypePrioritize(Guid categoryId, string type,bool prioritize=true)
        {
            return _context.Files.Where(x => x.CategoryId == categoryId && x.Type == type && x.IsPrioritize == prioritize).OrderByDescending(x => x.DateCreated).ThenByDescending(x => x.IsPrioritize).ToList();
        }
        public List<Files> GetAllFileByTypeCategory(List<Guid> categoryId, string type)
        {
            return _context.Files.Where(x => categoryId.Contains(x.CategoryId??Guid.Empty) && x.Type == type).OrderByDescending(x => x.DateCreated).ThenByDescending(x => x.IsPrioritize).ToList();
        }
        public List<Files> GetAllFileByTypeCategoryPrioritize(List<Guid> categoryId, string type, bool prioritize = true)
        {
            return _context.Files.Where(x => categoryId.Contains(x.CategoryId ?? Guid.Empty) && x.Type == type && x.IsPrioritize == prioritize && x.IsPublic == true).OrderByDescending(x => x.DateCreated).ThenByDescending(x => x.IsPrioritize).ToList();
        }
    }
}
