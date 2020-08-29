using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using LienPhatERP.Models;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace LienPhatERP.Controllers.Admin
{
    [Authorize(Policy = "RequireAdminRole")]
    public class FileAdminController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        public FileAdminController(UserManager<ApplicationUser> userManager
            ,IFileService fileService
            , ICategoryService categoryService)
        {
            _userManager = userManager;
            _fileService = fileService;
            _categoryService =categoryService;
        }
        public IActionResult Index(Guid cateId,Guid categoryId,string type,int page=1)
        {
            ViewBag.CateId = cateId;
            ViewBag.CategoryId = categoryId;
            ViewBag.Type = type;
            var pageSize = 10;
            var file = _fileService.GetAllFileByType(categoryId,type);
            var model= PagingList.Create(file, pageSize, page);
            model.RouteValue = new RouteValueDictionary(new { cateId= cateId, categoryId = categoryId, type = type });
            model.Action = "Index";
            return View(model);
        }
        public IActionResult CreateFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFile(FileViewModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var file = new Files()
                {
                    FileName = model.FileName,
                    Type = model.Type,
                    UserId = user.Id,
                    CategoryId = model.CategoryId
                };
                if (!string.IsNullOrEmpty(model.Url))
                {
                    file.Url = model.Url;
                    _fileService.Insert(file);
                    return Json(ResultStatus.ReturnTrue());
                }
                if (model.File != null)
                {
                    file.Url = await UploadFileCategory(model.File, model.CategoryId??Guid.Empty);
                    _fileService.Insert(file);
                    return Json(ResultStatus.ReturnTrue());
                }
                return Json(ResultStatus.ReturnFalse());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public IActionResult UpdateFile(Guid id)
        {
            var file = _fileService.GetFile(id);
            return View(file);
        }
        [HttpPost]
        public IActionResult UpdateFile(FileViewModel model)
        {
            try
            {
                var file = _fileService.GetFile(model.Id);
                var user = _userManager.GetUserAsync(User).Result;
                file.FileName = model.FileName;
                file.Url = model.Url;
                file.Type = model.Type;
                file.UserId = user.Id;
                file.CategoryId = model.CategoryId;
                _fileService.Update(file);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public IActionResult PublicFile(Guid id)
        {
            try
            {
                var file = _fileService.GetFile(id);
                var user = _userManager.GetUserAsync(User).Result;
                file.IsPublic = !file.IsPublic;
                _fileService.Update(file);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public IActionResult PrioritizeFile(Guid id)
        {
            try
            {
                var file = _fileService.GetFile(id);
                file.IsPrioritize = !file.IsPrioritize;
                _fileService.Update(file);
                return Json(ResultStatus.ReturnTrue());
                //if (!file.IsPrioritize??false)
                //{
                //    var f = _fileService.GetAllFileByTypePrioritize(file.CategoryId??Guid.Empty, "Image",true);
                //    //if (f.Count()<=14)
                //    //{
                //        var user = _userManager.GetUserAsync(User).Result;
                //        file.IsPrioritize = !file.IsPrioritize;
                //        _fileService.Update(file);
                //        return Json(ResultStatus.ReturnTrue());
                //    //}
                //    //else
                //    //{
                //    //    return Json( new
                //    //    {
                //    //        Result = true,
                //    //        Message = "Ưu tiên đã hết. Vui lòng tắt ưu tiên khác"
                //    //    });
                //    //}
                //}
                return Json(ResultStatus.ReturnFalse());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }

        [HttpPost]
        public async Task<string> UploadFileCategory(IFormFile file, Guid categoryId)
        {
            try
            {
                var cate = _categoryService.GetId(categoryId);
                string folder = $"UploadFiles/{cate.Slug}/{DateTime.Now:yyyy}/{DateTime.Now:MM}/";
                // full path to file in temp location
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    folder);
                bool folderExists = Directory.Exists(filePath);
                if (!folderExists)
                    Directory.CreateDirectory(filePath);
                var listUpload = "";
                var url = "";
                if (file.Length > 0)
                {
                    var fileName = file.FileName.Replace(" ", "");
                    var id = Guid.NewGuid();
                    using (var stream = new FileStream(filePath + $"{id}_{fileName}", FileMode.Create))
                    {
                        using (var image = Image.Load(file.OpenReadStream(), out IImageFormat format))
                        {
                            using (Image<Rgba32> destRound = image.Clone(x => x.ConvertToAvatar(new Size(425, 425), 1)))
                            {
                                destRound.Save(stream, format);
                                //await file.CopyToAsync(stream);
                            }
                        }
                        url = $"/{folder}{id}_{fileName}";
                        listUpload= url;
                    }
                }

                return listUpload;
            }
            catch
            {
                string message = $"file / upload failed!";
                return "";
            }
        }
    }
}
