using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public interface IFileService
    {
        Files Insert(Files entity);
        Files Update(Files entity);
        Files Delete(Files entity);
        Files GetFile(Guid id);
        List<string> GetFileName(string path);
        List<Files> GetAllFile();
        List<Files> GetAllFileByType(Guid categoryId,string type);
        List<Files> GetAllFileByTypePrioritize(Guid categoryId, string type, bool prioritize);
        List<Files> GetAllFileByTypeCategory(List<Guid> categoryId, string type);
        List<Files> GetAllFileByTypeCategoryPrioritize(List<Guid> categoryId, string type, bool prioritize = true);
    }
}
