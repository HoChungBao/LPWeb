using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;

namespace LienPhatERP.Controllers.Admin
{
    [Authorize(Policy = "RequireAdminRole")]
    public class HomeAdminController : Controller
    {
        private readonly IInformationService _informationService;
        public HomeAdminController(IInformationService informationService)
        {
            _informationService = informationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Infomation()
        {
            var infomation = _informationService.GetAllInfomation();
            return View(infomation);
        }
        public IActionResult CreateInfomation(long id)
        {
            var infomation =new InfomationModel(_informationService.GetInfomationById(id));
            return View(infomation);
        }
        [HttpPost]
        public IActionResult CreateInfomation(InfomationModel model)
        {
            try
            {
                var info = _informationService.GetInfomationById(model.Id);
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.MainPhone;
                info.Work = JsonHelper.SerializeObject(model.Work);
                info.About = model.About;
                info.MainPhone = model.MainPhone;
                _informationService.Update(info);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public IActionResult InsertList()
        {
            var infomation = new List<Infomation>();
            var work = new WorkModel()
            {
                InWeek = "Thứ hai - thứ sáu: 8:30 - 17:30",
                Saturday = "Thứ bảy: 8:30 - 12:00",
                WeedKed = "Chủ nhật: Nghỉ",
            };
            infomation.Add(new Infomation()
            {
                Id = 1,
                About = "Liên Phát là một trong những công ty chuyên lĩnh vực sản xuất các vật phẩm quảng cáo, Mô hình  quảng cáo, Event, Activation.",
                Address = "Lầu 4, 302 Lê Văn Sỹ, P. 1, Q. Tân Bình, TP.HCM",
                Email = "thoi.nguyenvan@lienphatad.com.vn",
                MainPhone = "093.8468.719",
                Phone = "093.8468.719",
                Work = JsonHelper.SerializeObject(work),
            });
            work = new WorkModel()
            {
                InWeek = "Mon - Fri: 8:30 am - 17:30 pm",
                Saturday = "Saturday: 8:30 am - 12:00 pm",
                WeedKed = "Sunday : Closed",
            };
            infomation.Add(new Infomation()
            {
                Id = 2,
                About = "Lien Phat is one of the companies specialized in the field of advertising products, Advertising, Event, Activation.",
                Address = "4th Floor, 302 Le Van Sy, Ward 1, Tan Binh Dist, HCMC",
                Email = "thoi.nguyenvan@lienphatad.com.vn",
                MainPhone = "093.8468.719",
                Phone = "093.8468.719",
                Work = JsonHelper.SerializeObject(work),
            });
                _informationService.InsertList(infomation) ;
            return View(infomation);
        }
    }
}
