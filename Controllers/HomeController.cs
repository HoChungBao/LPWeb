using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LienPhatERP.Models;
using LienPhatERP.Services;
using System.Linq;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.SignalR;
using LienPhatERP.Notification;
using System.Threading.Tasks;


namespace LienPhatERP.Controllers
{
    [ResponseCache(CacheProfileName = "default")]
    public class HomeController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        // private readonly IMySqlService _mySqlService;
        public HomeController(ICommonService commonService/*IMySqlService mySqlService*/
            ,ICategoryService categoryService
            , IFileService fileService)
        {
            _categoryService =categoryService;
            _fileService =fileService;
            _commonService = commonService;
            //_mySqlService = mySqlService;
        }
        //public HomeController(ICommonService commonService)
        //{
        //    _commonService = commonService;

        //}
         [ResponseCache(Duration = 60)]
      //  [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Index()
        {

            return View();
        }
        [ResponseCache(Duration = 60)]
        public IActionResult Service(string slug)
        {
            var fileModel= new FileServiceModel();
            if (slug == "*"||string.IsNullOrEmpty(slug))
            {
                var cate = _categoryService.GetCategoryBySlug("dich-vu");
                var categorie = _categoryService.GetAllSubCategory(cate.Id);
                fileModel.Category.AddRange(categorie);
                var file = _fileService.GetAllFileByTypeCategoryPrioritize(categorie.Select(x => x.Id).ToList(), "Image", true);
                fileModel.File.AddRange(file);
            }
            else
            {
                var cate = _categoryService.GetCategoryBySlug(slug);
                fileModel.Category.Add(cate);
                var file = _fileService.GetAllFileByTypePrioritize(cate.Id, "Image", true);
                fileModel.File.AddRange(file);
            }
            return View(fileModel);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult HomeEng()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult ServiceEng()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
          
            return View();
        }
   //     [ResponseCache(CacheProfileName = "default")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Success()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
       // [ResponseCache(CacheProfileName = "default")]
        public IActionResult Main()
        {
          //  ViewData["Message"] = "Your application description page.";

            return View();
        }
      //  [ResponseCache(CacheProfileName = "default")]
        public IActionResult MediaChanel()
        {
          

            return View();
        }
        public IActionResult Demo()
        {


            return View();
        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult DashboardCS()
        {
           
            return View();
        }
    }
}
