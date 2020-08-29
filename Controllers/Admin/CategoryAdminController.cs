using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LienPhatERP.Controllers.Admin
{
    [Authorize(Policy = "RequireAdminRole")]
    public class CategoryAdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryAdminController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            try
            {
                var cate = new Category()
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                };
                 _categoryService.Add(cate);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public IActionResult UpdateCategory(Guid id)
        {
            var cate= _categoryService.GetId(id);
            return View(cate);
        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            try
            {
                var cate = _categoryService.GetId(model.Id);
                if (cate.Name != model.Name)
                {
                    cate.Name = model.Name;
                    _categoryService.UpdateName(cate);
                    return Json(ResultStatus.ReturnTrue());
                }
                cate.CategoryId = model.CategoryId;
                cate.Description = model.Description;
                _categoryService.Update(cate);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public IActionResult DeleteCategory(Guid id)
        {
            try
            {
                var cate = _categoryService.GetId(id);
                cate.IsDeleted = !cate.IsDeleted;
                _categoryService.Update(cate);
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
        public IActionResult LockCategory(Guid id)
        {
            try
            {
                var cate = _categoryService.GetId(id);
                cate.IsLocked = !cate.IsLocked;
                _categoryService.Update(cate);
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
        public IActionResult PublishCategory(Guid id)
        {
            try
            {
                var cate = _categoryService.GetId(id);
                cate.IsPublished = !cate.IsPublished;
                _categoryService.Update(cate);
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
        public IActionResult GetCategory(Guid categoryId)
        {
            try
            {
                var cate = _categoryService.GetAllSubCategory(categoryId);

                return Json(new
                {
                    Result = true,
                    Data = cate
                });
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
    }
}
