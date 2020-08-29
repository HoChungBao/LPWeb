using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LienPhatERP.Models;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using LienPhatERP.Contants;
using Microsoft.AspNetCore.Hosting;
using LienPhatERP.Helper;
using LienPhatERP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RestSharp;
using System.Globalization;
using System.Security.Claims;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Remotion.Linq.Clauses.ExpressionVisitors;
using Microsoft.AspNetCore.SignalR;
using LienPhatERP.Notification;

namespace LienPhatERP.Controllers
{
    public class CommonController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ICommonService _commonService;
        private IHostingEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberService _memberService;
        private readonly IConfiguration _configuration;
        //private readonly IHubContext<ChatHub> _hubContext;
        public CommonController(
            UserManager<ApplicationUser> userManager,
            ICommonService commonService,
            IEmailSender emailSender,
        
            RoleManager<IdentityRole> roleManager,
            IHostingEnvironment env,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            IMemberService memberService
            /*IHubContext<ChatHub> hubContext*/)
        {
            _userManager = userManager;
            _commonService = commonService;
            _emailSender = emailSender;
            _logger = logger;
            _env = env;
            _roleManager = roleManager;
            _configuration = configuration;
            _memberService= memberService;
            //_hubContext = hubContext;


        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult RundataSample(string name)
        {
            if (User.IsInRole("Admin"))
            {
                var sampleContentFolder = Path.Combine(_env.ContentRootPath, "SQL");
                var filePath = Path.Combine(sampleContentFolder, name);
                var lines = System.IO.File.ReadLines(filePath);
                var commands = _commonService.ParseCommand(lines);
                _commonService.RunCommands(commands);
            }
            return View();
        }

        public IActionResult CreateProductType()
        {
            return View();
        }

      


        /// <summary>
        /// Hàm lấy data Thành phố từ file data.json
        /// gọi jsonHelper DeserializeObject theo list
        /// </summary>
        /// <returns></returns>
        public JsonResult Province()
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "assets/demo/default/base/province.json";
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {

                    var datas = SourceReader.ReadToEnd();

                    var jsondata = JsonHelper.DeserializeObject<List<ProvinceViewModel>>(datas);

                    return Json(new
                    {
                        provinces = jsondata
                    });
                }

            }
            catch (Exception e)
            {
                return Json(new
                {

                });
            }
        }

      

        /// <summary>
        /// Hàm upload File
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
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

        public static async Task<List<UploadFileApiResult>> UploadFiles(ICollection<IFormFile> files,
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
            var listUpload = new List<UploadFileApiResult>();
            var url = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var id = Guid.NewGuid();
                    using (var stream = new FileStream(filePath + $"{id}_{formFile.FileName}", FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                        url = folder + $"{id}_{formFile.FileName}";
                        listUpload.Add(new UploadFileApiResult
                        {
                            FileName = formFile.FileName,
                            Url = url
                        });
                    }
                }
            }

            return listUpload;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileVideo(IFormFile file)
        {

            try
            {


                string folder = $"UploadFiles/Videos/";
                // full path to file in temp location
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    folder);

                bool folderExists = Directory.Exists(filePath);
                if (!folderExists)
                    Directory.CreateDirectory(filePath);
                var listUpload = new UploadFileApiResult();
                var url = "";

                if (file.Length > 0)
                {
                    var id = Guid.NewGuid();
                    using (var stream = new FileStream(filePath + $"{id}_{file.FileName}", FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        url = url + folder + $"{id}_{file.FileName}";
                        listUpload.FileName = file.FileName;
                        listUpload.Url = url;
                    }
                }

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

        private List<ProvinceViewModel> GetProvinces()
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "assets/demo/default/base/province.json";
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {

                    var datas = SourceReader.ReadToEnd();

                    var jsondata = JsonHelper.DeserializeObject<List<ProvinceViewModel>>(datas);

                    return jsondata;
                }

            }
            catch (Exception e)
            {
                return new List<ProvinceViewModel>();
            }
        }

      
       
        [HttpPost]
        public async Task<IActionResult> UploadFileImage(IFormFile file)
        {

            try
            {


                string folder = $"UploadFiles/{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}/";
                // full path to file in temp location
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    folder);

                bool folderExists = Directory.Exists(filePath);
                if (!folderExists)
                    Directory.CreateDirectory(filePath);
                var listUpload = new UploadFileApiResult();
                var url = "";

                if (file.Length > 0)
                {
                    var id = Guid.NewGuid();
                    using (var stream = new FileStream(filePath + $"{id}_{file.FileName}", FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        url = url + folder + $"{id}_{file.FileName}";
                        listUpload.FileName = file.FileName;
                        listUpload.Url = url;
                    }
                }

                return Json(new
                    {
                        Message = ResultStatus.Success,
                        Data = $"/{listUpload.Url}"
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

        [HttpPost]
        public async Task<IActionResult> UploadFilesToCnd(IFormFile file)
        {
            List<UploadFileApi> list = new List<UploadFileApi>();
            UploadFileApi data = new UploadFileApi();
            data.FileName = file.FileName;
            byte[] files;
            using (Stream inputStream = file.OpenReadStream())
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }

                files = memoryStream.ToArray();
                data.Data = files;
            }

            list.Add(data);


            string apiRequest = string.Format("api/CndUploadImages"); // -> [Route("api/UploadImages")]

            var urlApi = "https://cdn2.medihub.vn/";
            var client = new RestClient(urlApi);

            var request = new RestRequest(apiRequest, Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(list);
            var rs1 = client.Execute<HttpContentResult<List<UploadFileApiResult>>>(request).Data;


            return Json(new
                {
                    Message = ResultStatus.Success,
                    Data = $"{rs1.Data.FirstOrDefault()?.Url}"
                }
            );
        }

      
        [HttpGet]
        public IActionResult AppointmentMeetingRoom()
        {
            var rs = _commonService.GetListAppointment("").Select(x => new MeetingRoomViewModel()
            {
                id = x.Id,
                room = x.Room,
                title = x.Name,
                start = x.AppointmentDate.ToString("yyyy/MM/dd h:mm tt"),
                end = x.AppointmentDate.AddHours(x.Durations).ToString("yyyy/MM/dd h:mm tt"),
                description =
                    $"Từ {x.Hour},  {DateTime.Parse(x.Hour).AddHours(x.Durations).ToString("h:mm tt")}, Người đặt: {x.UserCreatedNavigation.Name}",
            });
            return View(rs.ToList());
        }

        [HttpDelete]
        public IActionResult DeleteAppointmentMeetingRoom(int id)
        {
            if (User.IsInRole(RoleBase.Admin.ToString()) || User.IsInRole(RoleBase.Hrm.ToString()))
            {

                _commonService.DeteteAppointMentMeetingRoom(id);
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            var book =_commonService.GetAppointMentMeetingRoom(id);
            if (!book.UserCreated.Equals(user.Result.Id))
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            _commonService.DeteteAppointMentMeetingRoom(id);
            return Json(new
            {
                Result = true,
                Message = ResultStatus.Success
            });
        }
        [HttpGet]
        [Authorize(Policy = "RequireInputData")]
        public IActionResult CreateMediHubAsset(long id = 0, string address = "", string speciality = "")
        {
            var rs = new MediHubAsset();
            rs.CsScreenId = id;
            rs.Address = address;
            rs.Specicality = speciality;
            return View(rs);
        }

       
        [HttpPost]
        public IActionResult UpdateMeetingRoom(MeetingRoomModel model)
        {
            try
            {
                model.AppointmentDate = DateTime.ParseExact(model.AppointmentDateStr,"dd/MM/yyyy", CultureInfo.InvariantCulture);
                _logger.LogCritical($"Ngày:{model.AppointmentDate}");
                _logger.LogCritical($"DAte:{model.AppointmentDate.Date}");
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                model.UserCreated = user.Id;
                if (model.Id != 0)
                {
                    var app = _commonService.GetAppointMentMeetingRoom(model.Id);
                    if(app == null) return Json(new
                    {
                        Result = false,
                        Message = ResultStatus.Fail
                    });
                    var hourCheck = DateTime.Parse(app.Hour);
                    var updateDate = model.AppointmentDate.AddSeconds(hourCheck.Hour * 3600 + hourCheck.Minute * 60);
                    var check = _commonService.CheckAppointMentMeetingRoom(updateDate);
                    if (check)
                    {
                        app.AppointmentDate = updateDate;
                        var updateApp = _commonService.UpdateAppointMentMeetingRoom(app);
                        var fromDate = DateTime.Parse(updateApp.Hour);
                        var todate = fromDate.AddHours(updateApp.Durations);
                        return Json(new
                        {
                            Result = new
                            {
                                title = model.AppointmentDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                                id = updateApp.Id.ToString(),
                                description = $"Từ {app.Hour} - {todate.ToString("h:mm tt")}, Người đặt: {user.Name}",

                            },
                            Message = ResultStatus.Success
                        });
                    }
                    return Json(new
                    {
                        Result = false,
                        Message = "Chưa thay đổi được, có thể bị trùng thời gian đặt"
                    });

                }
                model.CreatedDate = DateTime.Now;
                var hour = DateTime.Parse(model.Hour);
     
                model.AppointmentDate =  model.AppointmentDate.AddSeconds(hour.Hour*3600+ hour.Minute * 60);
                var checkInsert = _commonService.CheckAppointMentMeetingRoom(model.AppointmentDate);
                if (checkInsert)
                {
                    var rs = _commonService.InsertAppointMentMeetingRoom(Mapper.Map<BookingMeetingRoom>(model));
                    var to = hour.AddHours(model.Durations);
                    return Json(new
                    {
                        Result = new
                        {
                            title = model.AppointmentDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                            id = rs.Id.ToString(),
                            description = $"Từ {model.Hour} -  {to.ToString("h:mm tt")}, Người đặt: {user.Name}",

                        },
                        Message = ResultStatus.Success
                    });
                }
              
                return Json(new
                {
                    Result = false,
                    Message = "Chưa đặt lịch được, có thể bị trùng thời gian đặt"
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult BusinessTrip(string startDate)
        {
            try
            {
                DateTime date = DateTime.Now;
                var f = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) :new DateTime(date.Year, date.Month, 1);
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                if (string.IsNullOrEmpty(user.Result.Id))
                {
                    return NotFound();
                }
                var rs1 = _commonService.GetAllBusinessTrip(user.Result.Id).Where(x=> x.CreatedDate> f).ToList();
                ViewData["1"] = f.ToString("yyyy-MM-dd");
                return View(rs1);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult BusinessTripAdmin(string startDate, string endDate, string key)
        {
            try
            {
                DateTime date = DateTime.Now;
                var f = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) : new DateTime(date.Year, date.Month, 1);
                var d = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : f.AddMonths(1).AddDays(-1);
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                ViewData["1"] = f.ToString("yyyy-MM-dd");
                ViewData["2"] = d.ToString("yyyy-MM-dd");
                ViewData["3"] = key;
                if (user.Result.IsManager)
                {
                    var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                    if (role != null)
                    {
                        var rs = _commonService.GetAllBusinessTripAdmin(f, d, role.Value, key);
                        return View(rs);
                    }
                    //var rs = _commonService.GetAllBusinessTripAdmin(f, d, user.Result..Value, key);
                    //   return View(rs);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Hrm")]
        public IActionResult GetAllBusinessTrip(string startDate, string endDate, string department,string key)
        {
            try
            {
                DateTime date = DateTime.Now;
                var f = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) : new DateTime(date.Year, date.Month, 1);
                var d = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : f.AddMonths(1).AddDays(-1);
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                ViewData["1"] = department;
                ViewData["2"] = f.ToString("yyyy-MM-dd");
                ViewData["3"] = d.ToString("yyyy-MM-dd");
                ViewData["4"] = key;
                if (User.IsInRole("Hrm") || User.IsInRole("Admin"))
                {
                    var rs1 = _commonService.GetAllBusinessTripAdmin(f, d, department, key);
                    return View(rs1);

                }
                if (user.Result.IsManager)
                {
                    var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                    if (role != null)
                    {
                        var rs = _commonService.GetAllBusinessTripAdmin(f, d, role.Value, key);
                        ViewData["1"] = role.Value;
                        return View(rs);
                    }
                }
                return NotFound();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult CreateBusinessTrip()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBusinessTrip(BusinessTripModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                model.UserCreated = user.Id;
                var rs= _commonService.InsertBusinessTrip(Mapper.Map<BusinessTrip>(model));
                model.Id = rs.Id;
              
                var urldomain = Request.Scheme + "://" + Request.Host.Value;
                if (user.IsManager)
                {
                    await SendmailToApproved(model, urldomain, _configuration["Email:Boss"]);
                }
                var email = _commonService.GetEmailUser(user.CompanyName);
                foreach (var item in email)
                {
                    await SendmailToApproved(model, urldomain, item.Email);
                }
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            }
        }

        private async Task SendmailToApproved(BusinessTripModel rs, string urldomain, string email)
        {
            var messageBodyCus = "";
            MessageBody("BusinessTrip.html", out messageBodyCus);
            messageBodyCus = messageBodyCus.Replace("{0}", rs.Name)
                .Replace("{1}", rs.Customer)

                .Replace("{2}", rs.Timer)
                .Replace("{3}", rs.Address)
                .Replace("{4}", rs.Contact)
                .Replace("{5}", rs.Phone)
                .Replace("{6}", rs.Note)             
                .Replace("{7}",
                    $"{urldomain}/Common/UpdateApproveBusinessTrip?id={rs.Id}");         
            await _emailSender.SendEmailAsync(email, "Mail thông báo tự động",
                messageBodyCus); // order --> send mail cho client service           
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdateApproveBusinessTrip(int id)
        {
            var rs = _commonService.GetBusinessTripById(id);
            if (rs.Status.Equals("0"))
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                rs.Status = "1";
                rs.UserApproved = user.Id;
                _commonService.UpdateBusinessTrip(rs);
                ViewData["Result"] = "Duyệt thành công";
            }
            else
            {
                ViewData["Result"] = "Bạn đã duyệt rồi";
            }       

            return View(rs);
        }

        private void MessageBody(string templateEmail, out string messageBody)
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "Templates"
                             + Path.DirectorySeparatorChar.ToString()
                             + "EmailTemplate"
                             + Path.DirectorySeparatorChar.ToString()
                             + templateEmail;
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            messageBody = builder.HtmlBody;
        }

        [HttpGet]
        public IActionResult UpdateBusinessTrip(int id)
        {
            var rs = _commonService.GetBusinessTripById(id);
            return View(rs);
        }
        [HttpPost]
        public IActionResult UpdateBusinessTrip(BusinessTripModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var rs = _commonService.GetBusinessTripById(model.Id);
                rs.Note = model.Note;
                rs.Address = model.Address;
                rs.Name = model.Name;
                rs.Contact = model.Contact;
                rs.Customer = model.Customer;
                rs.Timer = model.Timer;
                rs.Phone = model.Phone;
                _commonService.UpdateBusinessTrip(rs);
             
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            }
        }
        [HttpGet]
        public IActionResult DeleteBusinessTrip(int id)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                _commonService.DeleteBusinessTrip(id);
              
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });              
            }
        }
    
        [HttpGet]
        public IActionResult FileUpload()
        {
            return View();
        }
        public JsonResult GetDataFile()
        {
            var rs = _commonService.GetAllFile();
            return Json(rs);
        }
        [HttpGet]
        public IActionResult FileUploadUser()
        {
            var rs = _commonService.GetAllFile().Where(x => x.IsPublic == true).ToList();
            return View(rs);
        }
        [HttpPost]
        public async Task<IActionResult> FileUpload(FileModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (model.Files != null)
                {
                    var rs = await UploadFiles(model.Files);
                    if (rs.Count() > 0)
                    {
                        Files f = new Files()
                        {
                            IsPublic = model.IsPublic,
                            FileName = model.Name,
                            Url = JsonHelper.SerializeObject(rs),
                            UserId = user.Id,
                            UserName = user.UserName
                        };
                        _commonService.InsertFile(f);
                    }
                }
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFile(FileModel model)
        {
            try
            {
                var rs = _commonService.GetFileById(model.Id);
                rs.IsPublic = model.IsPublic;
                if (model.Files != null)
                {
                    var f = await UploadFiles(model.Files);
                    if (f.Count() > 0)
                    {
                        rs.FileName = model.Name;
                        rs.Url = rs.Url.Replace("]", JsonHelper.SerializeObject(f).Replace("[", ","));
                    }
                }
                _commonService.UpdateFile(rs);
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            }
        }
        [HttpGet]
        public IActionResult ReadFileUpload(string id, string filename)
        {
            try
            {
                var rs = _commonService.GetFileById(id);
                var f = JsonHelper.DeserializeObject<List<UploadFileApiResult>>(rs.Url).FirstOrDefault(x => x.FileName.Equals(filename));
                if (f != null)
                {
                    var extention = filename.Split(".");
                    var valid = new List<string> { "jpg", "bmp", "gif", "png" ,"jpeg"};
                    if (valid.Contains(extention[extention.Length - 1]))
                    {
                        return LocalRedirect($"~/{f.Url}");
                    }
                    var filepath = $"wwwroot/{f.Url}";
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                    return File(fileBytes, "application/msdownload", f.FileName);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]   
        public IActionResult DeleteFileUpload(string id, string filename)
        {
            try
            {
                var rs = _commonService.GetFileById(id);
                var f = JsonHelper.DeserializeObject<List<UploadFileApiResult>>(rs.Url);
                var k= f.FirstOrDefault(x => x.FileName.Equals(filename));
                if (k != null)
                {
                    f.Remove(k) ;
                    if (f.Count() > 0)
                    {
                        rs.Url = JsonHelper.SerializeObject(f);
                        _commonService.UpdateFile(rs);
                    }
                    else
                    {
                        _commonService.DeleteFile(rs.Id.ToString());
                    }
                    return Json(new
                    {
                        Result = true,
                        Message = ResultStatus.Success
                    });
                }
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
                });
            }
        }
        #region File

        [HttpPost]
        public async Task<IActionResult> ReadFileExcelMediHubAssert(IFormFile file)
        {         
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                var i= ReadFileExcel(file,row =>
                {
                    var item = new MediHubAsset()
                    {
                        CsScreenId = Convert.ToInt64(checkNull(row.GetCell(0))),
                        Name = checkNull(row.GetCell(1)),
                        Address = checkNull(row.GetCell(2)),
                        Tvbrand = checkNull(row.GetCell(3)),
                        Tvsize = checkNull(row.GetCell(4)),
                        Seri = checkNull(row.GetCell(5)),
                        Box = checkNull(row.GetCell(6)),
                        Timer = checkNull(row.GetCell(7)),
                        Note = checkNull(row.GetCell(8)),
                        Specicality = checkNull(row.GetCell(9)),
                        UserUpdated = user
                    };
                });
                return Json(new
                {
                    Result = true,
                    Message =i
                });
            }
            catch (Exception ex)
            {               
                return Json(new
                {
                    Result = false,
                    Message = ResultStatus.Fail
            });
            }
        }
        public IActionResult ReadFileExcelDsPlayer(IFormFile file)
        {
            try
            {
                var rs= ReadFileExcel(file, row =>
                {
                    var item = new DsPlayer()
                    {
                        PlayerId = checkNull(row.GetCell(1)),
                        PlayerName = checkNull(row.GetCell(1)),
                        Address = checkNull(row.GetCell(1)),
                        IsOffline = Convert.ToBoolean(checkNull(row.GetCell(1))),
                        Note = checkNull(row.GetCell(1)),
                        CurrentStatus = checkNull(row.GetCell(1)),
                        IsHospital =Convert.ToBoolean(checkNull(row.GetCell(1))),
                        Status = checkNull(row.GetCell(1)),
                        UserName = checkNull(row.GetCell(1)),
                    };
                }) ;
                return Json(new
                {
                    Result = true,
                    Message = rs
                }) ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<string> ReadFileExcel(IFormFile file,Action<IRow> func)
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
                            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue;
                                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                                func(row);
                            }
                            System.IO.File.Delete(fullPath);
                            return ResultStatus.Success;
                       
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
   
        private async Task<IActionResult> Download(string name, string f,Action<ISheet> func)
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
        #endregion
        private string checkNull(object c)
        {
            return c != null ? c.ToString() : "";
        }
        public async Task Chat(string message)
        {
            var _hubContext = (IHubContext<ChatHub>)ControllerContext.HttpContext.RequestServices
                            .GetService(typeof(IHubContext<ChatHub>));

            //var session = HttpContext.Session.GetString("notifi");
            //if (session != null)
            //{
            //    var notification= JsonHelper.DeserializeObject<List<string>>(session);
            //    notification.Insert(0,message);
            //    message = JsonHelper.SerializeObject(notification);
            //    HttpContext.Session.SetString("notifi", message);
            //    await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            //    return;
            //}
            //var notification = new List<string>();
            //notification.Add(message);
            //HttpContext.Session.SetString("notifi", JsonHelper.SerializeObject(new List<string>() { message }));
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"<a href='#'>{message}</a>");
        }
    }
}