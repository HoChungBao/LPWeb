using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data;
using GoogleMaps.LocationServices;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using LienPhatERP.Models;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LienPhatERP.Controllers
{
    [Authorize]
    public class OutletController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private IHostingEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberService _memberService;
        private readonly IOutletStoreService _drugService;
        public OutletController(
           UserManager<ApplicationUser> userManager,
           IEmailSender emailSender,
           RoleManager<IdentityRole> roleManager,
           IHostingEnvironment env,
           ILogger<OutletController> logger,
           IMemberService memberService,
           IOutletStoreService drugService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _env = env;
            _roleManager = roleManager;
            _memberService = memberService;
            _drugService= drugService;

        }
        #region Component
        [HttpGet]
        public IActionResult Component()
        {
            return View();
        }
        public IActionResult GetDataComponent()
        {
            var rs = _drugService.GetAllComponent();
            return Json(rs);
        }
        [HttpGet]
        public IActionResult CreateComponent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateComponent(MComponentModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                model.CreatedBy = user.UserName;
                model.UpdatedBy = user.UserName;
                model.Slug = StringUtils.CreateUrl(model.Name);
                _drugService.InsertComponent(Mapper.Map<MComponent>(model));
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult UpdateComponent(int id)
        {
            var rs = _drugService.GetComponentById(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateComponent(MComponentModel model)
        {
            try
            {
                var rs = _drugService.GetComponentById(model.Id);
                rs.UpdatedBy = _userManager.GetUserAsync(User).Result.UserName;
                rs.Slug = StringUtils.CreateUrl(model.Name);
                rs.IsDeleted = model.IsDeleted;
                _drugService.UpdateComponent(rs);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        #endregion

        #region DrugLabel
        [HttpGet]
        public IActionResult DrugLabel()
        {
            return View();
        }
        public IActionResult GetDataDrugLabel()
        {
            var rs = _drugService.GetAllDrugLabel();
            return Json(rs);
        }
        [HttpGet]
        public IActionResult CreateDrugLable()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDrugLabel(MDrugLabelModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                model.CreatedBy = user.UserName;
                model.UpdatedBy = user.UserName;
                model.Slug = StringUtils.CreateUrl(model.Name);
                _drugService.InsertDrugLabel(Mapper.Map<MDrugLabel>(model));
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult UpdateDrugLabel(int id)
        {
            var rs = _drugService.GetDrugLabelById(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateDrugLabel(MDrugLabelModel model)
        {
            try
            {
                var rs = _drugService.GetDrugLabelById(model.Id);
                rs.UpdatedBy = _userManager.GetUserAsync(User).Result.UserName;
                rs.Slug = StringUtils.CreateUrl(model.Name);
                rs.IsDeleted = model.IsDeleted;
                _drugService.UpdateDrugLabel(rs);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        #endregion
        #region Outlet
        public IActionResult DrugProvince()
        {
            return Json(Province.GetProvinces(_env));
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(DrugProvince());
        }
        public IActionResult GetDataOutletStore()
        {
            var drug = _drugService.GetAllDrugStore().GroupBy(x => new { x.DrugStoreName, x.DrugStoreAddress, x.Ward, x.District, x.Province }).Select(x =>new MOutletStoreProjectDetailModel(x.First())).ToList();
            var project = _drugService.GetAllProjectDetailMediGroup().Select(x => new ProjectDetailModel(x)).ToList();
            foreach(var item in drug)
            {
                item.ProjectDetail = project.Where(x => x.MediGroupCode.Equals(item.MediGroupCode)).ToList();
            }
            return Json(drug);
        }
        public IActionResult GetJsonDataOutletStore(string term)
        {
            var drug = _drugService.GetAllDrugStorebyName(term).Select(x=> new {Id= x.Id,label = x.DrugStoreName, category = x.Province +" - " + x.District});
           
            return Json(drug);
        }
        [HttpGet]
        public IActionResult CreateOutletStore()
        {
            return View(DrugProvince());
        }
        [HttpPost]
        public IActionResult CreateOutletStore(MOutletStoreModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                model.CreatedBy = user.UserName;
                model.UpdatedBy = user.UserName;
                model.MediGroupCode = StringUtils.GetRandomCode(model.Area);
                model.Ward = StringUtils.RemoveVietNam(model.Ward).ToUpper();
                model.District = StringUtils.RemoveVietNam(model.District).ToUpper();
                model.Province = StringUtils.RemoveVietNam(model.Province).ToUpper();
                model.Area = StringUtils.RemoveVietNam(model.Area).ToUpper();
                model.DrugStoreName = StringUtils.RemoveVietNam(model.DrugStoreName);
                model.DrugStoreAddress = StringUtils.RemoveVietNam(model.DrugStoreAddress).ToUpper();
                _drugService.InsertDrugStore(Mapper.Map<MOutletStore>(model));
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult DrugProject(string medigroup)
        {
            ViewData["Medigroup"] = medigroup;
            return View();
        }
        [HttpGet]
        public IActionResult GetDataDrugProjectDetail(string medigroup)
        {
            var rs = _drugService.GetProjectDetailByMediGroup(medigroup).
                Select(x=>new{ Id = x.Id, Component = x.Component, Label = x.Label
                , Position = x.Position, Hsize = x.Hsize, Vsize = x.Vsize,  NumOfArea =x.NumOfArea
                , Unit = x.Unit, CostPayForDrugStore = x.CostPayForDrugStore
                , CostPayForProduction = x.CostPayForProduction, DateBeginRent = x.DateBeginRent
                , MonthRent = x.MonthRent, Images = x.Images, DrugName = x.DrugName
                , District = x.District, Province = x.Province, Area = x.Area
                , Address = x.Address, MediGroupCode = x.MediGroupCode
                , Ward = x.Ward, IsMediGroup = !string.IsNullOrEmpty(x.MediGroupCode)
                , ProjectName = x.Project.Name, ProjectId = x.Project.Id, ProjectCustomerId = x.Project.CustomerId }).ToList();
            return Json(rs);
        }
        public IActionResult UpdateOutletStore(int id)
        {
            var rs = _drugService.GetDrugStoreById(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateOutletStore(MOutletStoreModel model)
        {
            try
            {
                var rs = _drugService.GetDrugStoreById(model.Id);
                rs.UpdatedBy = _userManager.GetUserAsync(User).Result.UserName;
                model.Update(rs);
                rs.MediGroupCode = StringUtils.GetRandomCode(model.Area);
                _drugService.UpdateDrugStore(rs);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult ReportOutlet()
        {
            return View(DrugProvince());
        }
        public IActionResult GetDataReportOutlet(ReportModel model)
        {
            try
            {
                var rs = _drugService.GetAllOutletReport(model);
                if (rs.Any())
                {
                    return Json(rs);
                }
                return Json(new List<OutletReportModel>());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new List<OutletReportModel>());
            }
        }
        [HttpGet]
        public IActionResult UpdateOutletStoredByUser()
        {
            try
            {
            var user = _userManager.GetUserAsync(User).Result;
            if(User.IsInRole("Admin")||(user.IsManager&& User.IsInRole("")))
            {
                var rs = _drugService.GetAllOutletStoreUserByUpdate();
                rs.ForEach(x => {
                    x.Updated = false;
                    x.UpdatedDate = DateTime.Now;
                }) ;
                var outlet = _drugService.GetrugStoreByListId(rs.Select(x => x.Id).ToList());
                outlet.ForEach(x =>
                {
                    var item = rs.FirstOrDefault(i => i.Id == x.Id);
                    x.Area = item.Area;
                    x.AvatarUrl = item.AvatarUrl;
                    x.BankAccount = item.BankAccount;
                    x.DrugStoreAddress = item.DrugStoreAddress;
                    x.MediGroupCode = item.MediGroupCode;
                    x.District = item.District;
                    x.DrugStoreName = item.DrugStoreName;
                    x.Latitue = item.Latitue;
                    x.Longtitue = item.Longtitue;
                    x.Note = item.Note;
                    x.Province = item.Province;
                    x.UpdatedDate = DateTime.Now;
                    x.StaffNumm = item.StaffNumm;
                    x.StandardLevelCode = item.StandardLevelCode;
                    x.Status = item.Status;
                    x.StoreOwner = item.StoreOwner;
                    x.StorePhoneNumber = item.StorePhoneNumber;
                    x.Type = item.Type;
                    x.UpdatedBy = user.UserName;
                    x.Ward = item.Ward;
                });
                _drugService.UpdateOutletListUser(outlet, rs);
                return Json(ResultStatus.ReturnTrue());
            }
            return Json(ResultStatus.ReturnFalse());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpPost]
        public IActionResult UpdateOutletStoredByUser(MOutletStoreModel model)
        {
            try
            {
                var outlet = _drugService.GetDrugStoreById(model.Id);
                outlet.UpdatedBy = _userManager.GetUserAsync(User).Result.UserName;
                model.Update(outlet);
                var outletuser = _drugService.GetOutletStoreUserById(model.Id);
                outletuser.Updated = false;
                outlet.MediGroupCode = StringUtils.GetRandomCode(model.Area);
                _drugService.UpdateOutletByUser(outlet, outletuser);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        #endregion
        #region User
        [HttpGet]
        public IActionResult UserAudit()
        {
            return View(DrugProvince());
        }
        public IActionResult GetDataUserAudit()
        {
            var drug = _drugService.GetAllDrugStoreActive().GroupBy(x => new { x.DrugStoreName, x.DrugStoreAddress, x.Ward, x.District, x.Province }).Select(x => new MOutletStoreProjectDetailModel(x.First())).ToList();
            var project = _drugService.GetAllProjectDetailMediGroup().Select(x => new ProjectDetailModel(x)).ToList();
            foreach (var item in drug)
            {
                item.ProjectDetail = project.Where(x => x.MediGroupCode.Equals(item.MediGroupCode)).ToList();
            }
            return Json(drug);
        }
        [HttpGet]
        public IActionResult OutletUser()
        {
            return View(DrugProvince());
        }
        public IActionResult GetDataOutletUser()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (User.IsInRole("Admin")||(user.IsManager && User.IsInRole("")))
            {
                return Json(ConvertOutletStoreUser(_drugService.GetAllOutletStoreUser()));
            }
          
            return Json(ConvertOutletStoreUser(_drugService.GetAllOutletStoreUser(user.UserName)));
        }
        private List<MOutletStoreProjectDetailModel> ConvertOutletStoreUser(List<MOutletStoreUser> mOutletStoreUser)
        {
            try
            {
            var drug = mOutletStoreUser.GroupBy(x => new { x.DrugStoreName, x.DrugStoreAddress, x.Ward, x.District, x.Province }).Select(x => new MOutletStoreProjectDetailModel(x.First())).ToList();
                var project = _drugService.GetAllProjectDetailMediGroup().Select(x => new ProjectDetailModel(x)).ToList();
                foreach (var item in drug)
                {
                    item.ProjectDetail = project.Where(x => x.MediGroupCode.Equals(item.MediGroupCode)).ToList();
                }
                return drug;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult CreateOutletStoreByUser(UserAuditModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(model.UserId).Result;
                if (string.IsNullOrEmpty(model.UserId))
                {
                    return Json(ResultStatus.ReturnFalse());
                }
                //if (model.CheckAll)
                //{
                //    _drugService.CreateAllOutletByUser(user.UserName,User.Identity.Name);
                //    return Json(ResultStatus.ReturnTrue());
                //}
                _drugService.CreateOutletByUser(user.UserName, User.Identity.Name,JsonHelper.DeserializeObject<List<string>>(model.CheckOutlet));
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult UpdateOutletStoreUser(int id)
        {
            var rs = _drugService.GetOutletStoreUserById(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateOutletStoreUser(MOutletStoreUserModel model)
        {
            try
            {
                var rs = _drugService.GetOutletStoreUserById(model.Id);
                rs.UpdatedBy = _userManager.GetUserAsync(User).Result.UserName;
                model.Update(rs);
                rs.MediGroupCode = StringUtils.GetRandomCode(model.Area);
                _drugService.UpdateOutletStoreUser(rs);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult OutletDetail(long id)
        {

            if (id != 0)
            {
                var rs = _drugService.GetDrugStoreById(id);
                var listDetails = _drugService.GetAllProjectDetailMediGroupbyDrug(rs.MediGroupCode);
                var vm = new OutletViewModel() {Outlet = rs, Projects = listDetails};
                return View(vm);
            }
            return View();
        }
        #endregion
        #region Project
        [HttpGet]
        public IActionResult Project()
        {
            return View();
        }
        public IActionResult GetDataProject()
        {
            var rs = _drugService.GetAllProject();
            return Json(rs);
        }
        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProject(MProjectModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                model.CreatedBy = user.UserName;
                model.UpdatedBy = user.UserName;
                model.Slug = StringUtils.CreateUrl(model.Name);
                var project = Mapper.Map<MProject>(model);
                var projectDetail=_drugService.ProjectMediGroupCode(project, ReadFileExceProject(model.File)).Id;
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success,
                    Redirect = $"/Outlet/ProjectDetail?id={projectDetail}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var rs = _drugService.GetProjectDrug(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateProject(MProjectModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var project = _drugService.GetProjectInclude(model.Id);
                model.Update(project);
                project.UpdatedBy = user.UserName;
                project.Slug = StringUtils.CreateUrl(model.Name);
                if (model.File != null)
                {
                    _drugService.ProjectMediGroupCode(project, ReadFileExceProject(model.File));
                }
                _drugService.UpdateProjectDrug(project);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public List<MProjectDetail> ReadFileExceProject(IFormFile file)
        {
            try
            {
                return ReadFileExcel(file, sheet =>
                {
                    var mProjectDetail = new List<MProjectDetail>();
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        var item = new MProjectDetail()
                        {
                          
                            DrugName = StringUtils.RemoveVietNam((checkNull(row.GetCell(0)))).ToUpper(),
                            Address =StringUtils.RemoveVietNam(RemoveChar(checkNull(row.GetCell(1)))).ToUpper(),
                            Ward= StringUtils.RemoveVietNam(RemoveChar(checkNull(row.GetCell(2)))).ToUpper(),
                            District= StringUtils.RemoveVietNam(RemoveChar(checkNull(row.GetCell(3)))).ToUpper(),
                            Province = StringUtils.RemoveVietNam(RemoveChar(checkNull(row.GetCell(4)))).ToUpper(),
                            Area = StringUtils.RemoveVietNam(RemoveChar(checkNull(row.GetCell(5)))).ToUpper(),
                            Label = checkNull(row.GetCell(6)),
                            Component = checkNull(row.GetCell(7)),
                            NumOfArea = row.GetCell(8) != null ? row.GetCell(9).NumericCellValue : 0,
                            Hsize = row.GetCell(9) != null ? row.GetCell(9).NumericCellValue : 0,
                            Vsize = row.GetCell(10) != null ? row.GetCell(10).NumericCellValue : 0,
                        //    NumOfArea = row.GetCell(11) != null ? row.GetCell(11).NumericCellValue : 0,
                            Unit = "m2",
                            CostPayForDrugStore = Convert.ToDecimal(row.GetCell(11) != null ? row.GetCell(11).NumericCellValue : 0),
                            CostPayForProduction = Convert.ToDecimal(row.GetCell(12) != null ? row.GetCell(12).NumericCellValue : 0),
                            CostPayForLp = Convert.ToDecimal(row.GetCell(13) != null ? row.GetCell(13).NumericCellValue : 0),
                            DateBeginRent = DateTime.Parse(!string.IsNullOrEmpty(row.GetCell(14).ToString()) ? row.GetCell(14).ToString() :"1988-05-06"),
                            MonthRent = Convert.ToInt32(row.GetCell(15) != null ? row.GetCell(15).NumericCellValue : 0),
                            Images = row.GetCell(16).ToString()
                        };
                        mProjectDetail.Add(item);
                    }
                    return mProjectDetail.ToList();
                }).Result as List<MProjectDetail>;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region ProjectDetail
        [HttpGet]
        public IActionResult ProjectDetail(int id)
        {
            var rs = _drugService.GetProjectDrug(id);
            var d = (ICustomerService)ControllerContext.HttpContext.RequestServices.GetService(typeof(ICustomerService));
            ViewData["Id"] = rs.Id;
            ViewData["Project"] = rs.Name;
            ViewData["Customer"] = d.GetAllCustomer().FirstOrDefault(x => x.Id == rs.CustomerId).Name;
            return View(DrugProvince());
        }
        public IActionResult GetDataProjectDetail(int id)
        {
            var rs = _drugService.GetProjectDetailById(id);
            var countMediGroupNot = _drugService.GetProjectDetailByIdNotMediGroup(id).Count();
            var countMediGroup = rs.Count - countMediGroupNot;
            var projectDetail = rs.Select(x => new { Id = x.Id, Component = x.Component, Label = x.Label, Position = x.Position, Hsize = x.Hsize, Vsize = x.Vsize, Unit = x.Unit, CostPayForDrugStore = x.CostPayForDrugStore, CostPayForProduction = x.CostPayForProduction, DateBeginRent = x.DateBeginRent, MonthRent = x.MonthRent, Images = x.Images, DrugName = x.DrugName, District = x.District, Province = x.Province, Area = x.Area, Address = x.Address, MediGroupCode = x.MediGroupCode, Ward = x.Ward, IsMediGroup = !string.IsNullOrEmpty(x.MediGroupCode), CountMediGroup = countMediGroup, CountMediGroupNot = countMediGroupNot }).ToList() ;
            return Json(projectDetail);
        }
        public IActionResult ProjectDetailMediGroup(string medigroup,int id)
        {
            try
            {
                if (string.IsNullOrEmpty(medigroup)&& _drugService.ComparseMediGroup(medigroup)>0)
                {
                    var projectDetail= _drugService.GetProjectDetail(id);
                    projectDetail.MediGroupCode = medigroup;
                    _drugService.UpdateProjectDetail(projectDetail);
                    return Json(ResultStatus.ReturnTrue());                  
                }
                return Json(ResultStatus.ReturnFalse());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        public JsonResult DrugMediGroupCode(List<MProjectDetail> projectDetail)
        {
            try
            {
                var rs = _drugService.MapProjectDetail(projectDetail);
                _drugService.UpdateListProjectDetail(rs);
                return Json(new
                {
                    Result = true,
                    Message = "Đã tạo MediGroupCode"
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult CreateProjectDetail(int id,string name,string customer)
        {
            ViewData["Project"] = name;
            ViewData["Customer"] = customer;
            ViewData["Id"] = id;
            return View(DrugProvince());
        }
        [HttpPost]
        public IActionResult CreateProjectDetail(MProjectDetailModel model)
        {
            try
            {
                var projectDetail =Mapper.Map<MProjectDetail>(model) ;
                DrugMediGroupCode(new List<MProjectDetail> { projectDetail });
                _drugService.InsertProjectDetail(projectDetail);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public IActionResult UpdateProjectDetail(int id)
        {
            var rs = _drugService.GetProjectDetail(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateProjectDetail(MProjectDetailModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var projectDetail = _drugService.GetProjectDetail(model.Id);
                model.Update(projectDetail);
                if (model.Images != null)
                {
                    projectDetail.Images = model.Images;
                }
                if ((!string.IsNullOrEmpty(model.DrugName)&&!model.DrugName.Equals(projectDetail.DrugName)) || (!string.IsNullOrEmpty(model.Ward) && !model.Ward.Equals(projectDetail.Ward))|| (!string.IsNullOrEmpty(model.District) && !model.District.Equals(projectDetail.District)) || (!string.IsNullOrEmpty(model.Province) && !model.Province.Equals(projectDetail.Province)) || (!string.IsNullOrEmpty(model.Area) && !model.Area.Equals(projectDetail.Area)))
                {
                    DrugMediGroupCode(new List<MProjectDetail> { projectDetail });
                }
                _drugService.UpdateProjectDetail(projectDetail);
                return Json(ResultStatus.ReturnTrue());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateDrugProjectDetail(string project)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var rs = project.Split("-");
                if (rs[0].Equals("project"))
                {
                    var projectDetail= _drugService.GetProjectDetail(Convert.ToInt32(rs[1]));
                    await InsertDrugByProjectAsyncRefactor(new List<MProjectDetail> { projectDetail },user.UserName, rs[2]);
                    return Json(ResultStatus.ReturnTrue());
                }
                else if (rs[0].Equals("projectmany"))
                {
                    var projectD = _drugService.GetProjectDetailByIdNotMediGroup(Convert.ToInt32(rs[1]));
                    var mg= await InsertDrugByProjectAsyncRefactor(projectD, user.UserName, rs[2]);
                    return Json(new
                    {
                        Result = true,
                        Message = $"Đã tạo {mg} cửa hàng"
                    }) ;
                }
                return Json(ResultStatus.ReturnFalse());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(ResultStatus.ReturnFalse());
            }
        }
        private async Task<string> InsertDrugByProjectAsyncRefactor(List<MProjectDetail> projectD, string user, string type)
        {
            // lay distinct data tao nha thuoc
            var rs = new List<MOutletStore>();
            rs = projectD.Select(x => new { x.Address, x.Area, x.District, x.DrugName, x.Province, x.StoreOwner, x.StorePhoneNumber,x.Ward }).ToList()
                .Distinct().Select(a => new MOutletStore
                { Area = a.Area,DrugStoreAddress= a.Address,District= a.District,StorePhoneNumber = a.StorePhoneNumber,DrugStoreName=a.DrugName,Ward = a.Ward,Province = a.Province,
                Type = type,
                    MediGroupCode = StringUtils.GetRandomCode(a.Area),
                   CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Status = "1",
                     CreatedBy = user,
                    UpdatedBy = user,
                    Latitue = "0",
                    Longtitue = "0"

                }).ToList();
            // gan lai ma code  
            var listDetail = new List<MProjectDetail>();
            try
            {
               // var googleLocation = new GoogleLocationService("AIzaSyCv-VJuonOOmaXpJLsqenv0bUTskGAeBWQ&am");


                var p = rs.ToList();
                 rs.Clear();
                //var location123 = googleLocation.GetAddressesListFromAddress(string.Join(",", p.Select(x => $"{x.Address} {x.Ward} {x.District} {x.Province}").ToList()));
                // var rs = new List<MOutletStore>();
                foreach (var item in p)
                    {
                    projectD.Where(w => w.Area == item.Area 
                    && w.Address == item.DrugStoreAddress 
                    && w.District == item.District
                    && w.Province == item.Province
                    && w.DrugName == item.DrugStoreName).ToList().ForEach(f => f.MediGroupCode = item.MediGroupCode);
                  //  var local = $"{item.DrugStoreAddress} {item.Ward} {item.District} {item.Province}";
                       // var location = googleLocation.GetLatLongFromAddress($"{item.DrugStoreAddress} {item.Ward} {item.District} {item.Province}");

                        //if (location != null)
                        //{
                        //    item.Latitue = location.Latitude.ToString();
                        //    item.Longtitue = location.Longitude.ToString();

                        //}
                    rs.Add(item);
                    
                    }

                    _drugService.InsertDrugProjectDetail(projectD, rs);
                  
               
                return $"{rs.Count()}";
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return $"{rs.Count()}";
            }
        }
        #endregion

        private async Task<object> ReadFileExcel(IFormFile file, Func<ISheet, object> func)
        {
            string fullPath = "";
            try
            {
                if (file.Length > 0)
                {
                    string filename = Path.GetExtension(file.FileName).ToLower();
                    if (filename == ".xls" || filename == ".xlsx")
                    {
                        string folderName = "Upload";
                        string webRootPath = _env.WebRootPath;
                        string newPath = Path.Combine(webRootPath, folderName);
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                        ISheet sheet;
                        fullPath = Path.Combine(newPath, file.FileName);
                        using (var input = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(input);
                            input.Position = 0;
                            if (filename == ".xls")
                            {
                                HSSFWorkbook hssfwb = new HSSFWorkbook(input);
                                sheet = hssfwb.GetSheetAt(0);
                            }
                            else
                            {
                                XSSFWorkbook hssfwb = new XSSFWorkbook(input);
                                sheet = hssfwb.GetSheetAt(0);
                            }
                            //IRow headerRow = sheet.GetRow(0); //Get Header Row
                            //int cellCount = headerRow.LastCellNum;
                            var rs= func(sheet);
                            System.IO.File.Delete(fullPath);
                            return rs;

                        }
                    }
                    else
                    {
                        return "Vui lòng chọn file khác";
                    }
                }
                return ResultStatus.Fail;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
                return ResultStatus.Fail;
            }
        }
        private async Task<IActionResult> Download(string name, string f, Action<ISheet> func)
        {
            string fullPath = "";
            try
            {
                string sWebRootFolder = _env.WebRootPath;
                string sFileName = name;
                fullPath = Path.Combine(sWebRootFolder, sFileName);
                //string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                //FileInfo file = new FileInfo(fullPath);
                var memory = new MemoryStream();
                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook;
                    workbook = new XSSFWorkbook();
                    ISheet excelSheet = workbook.CreateSheet("Demo");

                    func(excelSheet);
                    workbook.Write(fs);
                }
                using (var stream = new FileStream(fullPath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
                return File(memory, f, sFileName);
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
                _logger.LogCritical(ex.ToString());
                return NotFound();
            }
        }
        private string checkNull(object c)
        {
            return c != null ? c.ToString().Trim() : "";
        }
        private string RemoveDrug(string text)
        {
            Regex pattern = new Regex("NHÀ THUỐC|NT|NHA THUOC|QUẦY THUỐC|QUAY THUOC|QT/g");
            return pattern.Replace(text, "").Trim().ToUpper();
        }
        private string RemoveChar(string text)
        {
            Regex pattern = new Regex(";|,|-|'/g");
            return pattern.Replace(text, " ").Trim().ToUpper();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(ICollection<IFormFile> files)
        {

            try
            {
                var listUpload = await UploadFiles(files);

                return Json(new
                {
                    Message = ResultStatus.Success,
                    Data = JsonHelper.SerializeObject(listUpload)
                }
                );
            }
            catch (Exception exp)
            {

                _logger.LogCritical($"Exception generated when uploading file - {exp.Message} ");
                string message = $"file / upload failed!";
                return Json(message);
            }
        }
        public static async Task<List<string>> UploadFiles(ICollection<IFormFile> files,
            string f = "UploadFiles")
        {
            string folder = $"{f}/{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}/";
            // full path to file in temp location
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                folder);

            bool folderExists = Directory.Exists(filePath);
            if (!folderExists)
                Directory.CreateDirectory(filePath);
            var listUpload = new List<string>();
            var url = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName= formFile.FileName.Replace(" ", "");
                    var id = Guid.NewGuid();
                    using (var stream = new FileStream(filePath + $"{id}_{fileName}", FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                        url = folder + $"{id}_{fileName}";
                        listUpload.Add(url);
                    }
                }
            }

            return listUpload;
        }
       
    }
}