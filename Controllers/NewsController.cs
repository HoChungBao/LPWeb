using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Models;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace LienPhatERP.Controllers
{
    [Authorize(Policy = "RequireContent")]
    [Route("[controller]/[action]")]
    public class NewsController : Controller
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        //private readonly ICommonService _commonService;
        private IHostingEnvironment _env;
        private readonly INewsService _newsService;

        public NewsController(
            UserManager<ApplicationUser> userManager,
            // RoleManager<IdentityRole> roleManager,
            IHostingEnvironment env,
            ILogger<AccountController> logger,
            INewsService newsService)
        {
            _userManager = userManager;
            _logger = logger;
            _env = env;
            _newsService = newsService;
        }


        #region News Post, Category

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> NewsPost()
        {
            var rs = _newsService.GetCategory();
            // var vm = Mapper.Map<NewsPostViewModel>(rs);
            return View(rs);
        }

        [HttpPost]
      //  [AllowAnonymous]
        public IActionResult NewsPost(NewsPostViewModel model)
        {
            try
            {
                //model.CategoryId=
                //var rs = _newsService.GetDataNewsPosts();

                //if (rs != null)
                //{
                //    model.IsPublished = true;
                //}
                //else
                //{
                //    model.IsPublished=false;
                //}
                //var check = model.IsPublished;
                //if (model.IsPublished == true)
                //{

                //}
                var user = _userManager.GetUserAsync(HttpContext.User);
                model.CreatedById = user.Result.Id;
                _newsService.InsertNewsPost(Mapper.Map<NewsPost>(model));
                return Json(new
                {
                    Result = "false",
                    Message = "Đã thêm thành công"
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = "false",
                    Message = ResultStatus.Fail
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNewCategory()
        {
            var vm = new CategotyViewModel();
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateNewCategory(CategotyViewModel model)
        {
            try
            {
                if (model.Name != null)
                {
                    _newsService.InsertCategory(Mapper.Map<Category>(model));
                }
                
                return Json(new
                {
                    Result = "true",
                    Message = "Đã thêm thành công"
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = "false",
                    Message = ResultStatus.Fail
                });
            }

        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> DetailNews(string slug)
        //{
        //    var rs = _orderService.GetNewsDetails("");
        //    // var vm = Mapper.Map<NewsPostViewModel>(rs);
        //    return View(rs);
        //}

        [HttpGet]
        public IActionResult DetailNewsPost()
        {
            
            return View();
        }



        [HttpGet("/news/{slug}", Name = "NewsShowDetail")]
        public async Task<IActionResult> NewsShowDetail(string  slug)
        {
            //slug = StringUtils.GetSafeHtml(slug);
            
            var vm = _newsService.GetNewsPostsBySlug(slug);
           
            return View(vm);
        }
        public JsonResult GetDataNewsPost()
        {
            var rs = _newsService.GetDataJsonNewsPost();
            //var vm = Mapper.Map<NewsPostViewModel>(rs);
            return Json(rs);
        }

        #endregion


        [HttpGet]
        public IActionResult EditNews(long id)
        {
            var news = _newsService.GetNewsById(id);
            //var categories = _newsService.GetCategories();
            //var vm = new EditNewsPost()
            //{
            //    newsPost = news,
            //    categories = categories
            //};
            return View(news);
        }
        [HttpPost]
        public IActionResult EditNews(NewsPostViewModel model)
        {
            var news = _newsService.GetNewsById(model.Id);
            if (news != null)
            {
                try
                {
                    var user = _userManager.GetUserAsync(HttpContext.User);
                    news.UpdatedById = user.Result.Id;
                    news.Name = model.Name;
                    news.IsPublished = model.IsPublished;
                    // news.Slug=
                    if (!string.IsNullOrEmpty(model.LinkImage))
                    {
                        news.LinkImage = model.LinkImage;
                    }
                    news.MetaDescription = model.MetaDescription;
                    news.FullContent = model.FullContent;
                    news.ShortContent = model.ShortContent;
                    news.CreatedOn = model.CreatedOn;
                    news.IsSticky = model.IsSticky;
                    _newsService.UpdateNewsPost(news);
                    return Json(new
                    {
                        Result = "true",
                        Message = "Đã sửa thành công",
                        Callback = $"/News/{news.Slug}"
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.ToString());
                    return Json(new
                    {
                        Result = "true",
                        Message = ResultStatus.Fail
                    });
                }
            }
            return Json(new
            {
                Result = "false",
                Message = ResultStatus.Fail
            });
        }

        [HttpGet]
        public IActionResult DeleteNewsPost(long id)
        {
            if (_newsService.DeteltePost(id))
            {
                return Json(new
                {
                    Result = "true",
                    Message = "Đã xóa thành công"
                });
            }
            return Json(new
            {
                Result = "false",
                Message = ResultStatus.Fail
            });

        }
    }
}