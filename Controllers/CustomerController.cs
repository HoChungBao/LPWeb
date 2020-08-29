using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LienPhatERP.Services;
using Microsoft.Extensions.Logging;
using LienPhatERP.ViewModels;
using AutoMapper;
using LienPhatERP.Entities;
using LienPhatERP.Contants;
using Microsoft.AspNetCore.Identity;
using LienPhatERP.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using LienPhatERP.Helper;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LienPhatERP.Controllers
{
    [Authorize(Policy = "RequireOrderRole")]
    public class CustomerController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IEmailSender _emailSender;
        private IHostingEnvironment _env;
        public CustomerController(ICommonService commonService, ICustomerService customerService, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger, IAuthorizationService authorizationService, IEmailSender emailSender,IHostingEnvironment env)
        {
            _commonService = commonService;
            _customerService = customerService;
            _logger = logger;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _emailSender = emailSender;
            _env = env;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertCustomer(CustomerModel model)
        {
            try
            {
                if (_customerService.GetAllCustomer().Where(x => (!string.IsNullOrEmpty(x.Email) && model.Email.Equals(x.Email)) 
                || (!string.IsNullOrEmpty(x.Email) && model.Phone.Equals(x.Phone))).ToList().Count > 0)
                {
                    return Json(new
                    {
                        Result = false,
                        Message = "Tên hoặc email bị trùng"
                    });
                }
                _customerService.InsertCustomer(Mapper.Map<Customer>(model));
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
        public IActionResult CreateCustomer()
        {
            var rs = _customerService.GetAllUser("Sale");
            return View(rs) ;
        }
        [HttpGet]
        public IActionResult CreateAgency()
        {
            var rs = _customerService.GetAllUser("Sale");
            return View(rs);
        }
        [HttpPost]
        public IActionResult InsertAgency(CustomerModel model)
        {
            try
            {
                if (_customerService.GetAllCustomer().Where(x => (!string.IsNullOrEmpty(x.Email) && model.Email.Equals(x.Email))
                                                                 || (!string.IsNullOrEmpty(x.Email) && model.Phone.Equals(x.Phone))).ToList().Count > 0)
                {
                    return Json(new
                    {
                        Result = false,
                        Message = "Tên hoặc email bị trùng"
                    });
                }
                model.IsAgency = true;
                _customerService.InsertCustomer(Mapper.Map<Customer>(model));
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
        public JsonResult GetDataCustomer()
        {
            var rs = Mapper.Map<List<CustomerViewModel>>(_customerService.GetAllCustomer());
            return Json(rs.Select(x=>new {x.Id,x.Contact,x.DateUpdated,x.DateCreated,x.DayCall,x.Email,x.Name,x.Note,x.Phone,x.Pic,x.Position,x.Status,x.Type,x.UserName,x.UnevenDay}));
        }
        [HttpGet]
        public IActionResult EditCustomer(long id)
        {
            var rs = _customerService.GetCustomerById(id);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (user.IsManager || user.Id == rs.UserPic)
            {
                ViewData["Name"] = _customerService.GetAllPic();
                return View(rs);
            }
            return new ForbidResult();
        }
        [HttpPost]
        public IActionResult UpdateCustomer(CustomerModel model)
        {
            try
            {
                var check = _customerService.GetAllCustomer().Where(x =>
                    x.Id != model.Id && ((!string.IsNullOrEmpty(x.Email) && model.Email.Equals(x.Email))
                    || (!string.IsNullOrEmpty(x.Email) && model.Phone.Equals(x.Phone)))).ToList();
                if (check.Count >0)
                    //if (_customerService.GetAllCustomer().Where(x => x.Id != model.Id && (x.Email.Equals(model.Email) || x.Phone.Equals(model.Phone))).ToList().Count > 0)
                {
                    return Json(new
                    {
                        Result = false,
                        Message = "Tên hoặc email bị trùng"
                    });
                }
                var rs = _customerService.GetCustomerById(model.Id);
                
                var oldemail = rs.UserPicNavigation?.Email;
                rs.Name = model.Name;
                rs.Note = model.Note;
                rs.Email = model.Email;
                rs.Contact = model.Contact;
                rs.Phone = model.Phone;
                rs.Position = model.Position;
                rs.Status = model.Status;
                if (!string.IsNullOrEmpty(model.Pic) || !model.Pic.Equals("Chọn nhân viên"))
                {
                  
                    if (!rs.Pic.Equals(model.Pic))
                    {
                        var user = _userManager.FindByIdAsync(model.Pic).Result;
                        rs.UserPic = model.Pic;
                        rs.Pic = user.Name;
                        //
                        var messageBody = "";
                        var urldomain = Request.Scheme + "://" + Request.Host.Value+ $"/Customer/EditCustomer?Id={rs.Id}";
                        MessageBody("NotifyChangeAssign.html", out messageBody);
                        messageBody = messageBody.Replace("{0}", rs.Name)
                            .Replace("{1}", "Số Điện thoại")
                            .Replace("{2}", rs.Phone)
                            .Replace("{3}",DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                            .Replace("{4}", urldomain));
                        if (!user.Email.Equals(oldemail))
                        {
                            _emailSender.SendEmailAsync(user.Email, "Thông báo: Phân công khách hàng chăm sóc", messageBody);
                            if (!string.IsNullOrEmpty(oldemail))
                            {
                                _emailSender.SendEmailAsync(oldemail, "Phân công khách hàng chăm sóc", messageBody);
                            }
                        }
                    
                   


                    }
                    
                }
               
                //if ((rs.Status.Equals("C") && model.Status.Equals("H")) || (rs.Status.Equals("F") && model.Status.Equals("C")))
                //{
                //    rs.Status = model.Status;
                //}
               

                _customerService.UpdateCustomer(rs);
                _commonService.LogAction(User.Identity.Name + " update customer", $"{rs.Name} ,  {rs.Note} ,{rs.Email} ,{rs.Contact},{rs.Phone},{rs.Pic},{rs.Status}");
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
        public IActionResult DeleteCustomer(long id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
              
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

        public IActionResult CustomerMail()
        {
            var rs = _customerService.GetAllCustomer().Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => new {Brand = x.Name, Email = x.Email}).ToList();
            return View(rs);
        }

        public IActionResult InsertCustomerMail(List<string> value,string name)
        {
            try
            {
                foreach (var item in value)
                {
                    SendmailToApproved(new BusinessTripModel(), name, item.Split(",").FirstOrDefault());
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
        public JsonResult GetDataCustomerAgency()
        {
            var rs = Mapper.Map<List<CustomerViewModel>>(_customerService.GetCustomerAgency());
            return Json(rs.Select(x => new { x.Id, x.Contact, x.DateUpdated, x.DateCreated, x.DayCall, x.Email, x.Name, x.Note, x.Phone, x.Pic, x.Position, x.Status, x.Type, x.UserName, x.UnevenDay }));
        }
        public IActionResult CustomerAgency()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertHistoryCustomer(HistoryModel model)
        {
            try
            {
                model.UserUpdate = _userManager.GetUserId(User);
                _customerService.InsertHistoryCustomer(Mapper.Map<History>(model));
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
        public async Task<IActionResult> HistoryById(long id,string userId)
        {
            //var userlogin = _userManager.GetUserAsync(HttpContext.User);
            //var user = new Document() { Author = userId };
            //var authorizationResult = await _authorizationService
            //    .AuthorizeAsync(User, user, Operations.Read);
            //if (userlogin.Result.IsManager == true || authorizationResult.Succeeded)
            //{
                var rs = _customerService.GetHistoryCustomerById(id);
                if (rs != null && rs.Count > 0)
                {
                    ViewData["Name"] = rs.First().Customer.Name;
                    ViewData["K"] = id;
                }

                return View(rs);
            //}
            //return new ForbidResult();

        }
        #region Contrack
        [HttpGet]
        public async Task<IActionResult> ContractById(long id,string userId)
        {
            var userlogin = _userManager.GetUserAsync(HttpContext.User);
            
                var user = new Document() { Author = userId };
                var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, user, Operations.Read);
                if (userlogin.Result.IsManager ==true || authorizationResult.Succeeded)
                {
                var rs = _customerService.GetAllContractById(id);
                    ViewData["K"] = id;
                    return View(rs);

            }
                else if (User.Identity.IsAuthenticated)
                {
                    return new ForbidResult();
                }
                else
                {
                    return new ChallengeResult();
                }
            
          
         
        }
        [HttpPost]
        public async Task<IActionResult> InsertContract(ContractModel model)
        {
            try
            {

                var user = _userManager.FindByNameAsync(User.Identity.Name);
             
                model.UserCreated = user.Result.Id;
                model.StartDate = DateTime.ParseExact(model.StrStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                model.EndDate = DateTime.ParseExact(model.StrEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var rs = Mapper.Map<List<PaymentContract>>(JsonHelper.DeserializeObject<List<PaymentContractModel>>(model.paymentContract));
                _customerService.InsertContract(Mapper.Map<Contract>(model), rs);
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
        public IActionResult UpdateContract(ContractModel model)
        {
            try
            {
                var rs = _customerService.GetContractById(model.Id);
                rs.Name = model.Name;
                rs.Note = model.Note;
                rs.Status = model.Status;
                rs.StartDate = model.StartDate;
                rs.EndDate = model.EndDate;
                if (!string.IsNullOrEmpty(model.Files) && !rs.Files.Equals(model.Files))
                {
                    var f = model.Files.Replace("[", "");
                    if (!string.IsNullOrEmpty(rs.Files))
                        rs.Files = rs.Files.Replace("]", "," + f);
                    else
                        rs.Files = model.Files;
                }
                var arr =Mapper.Map<List<PaymentContract>>(JsonHelper.DeserializeObject<List<PaymentContractModel>>(model.paymentContract));
                foreach (var item in rs.PaymentContract)
                {
                    var d = arr[0];
                    item.Status = d.Status;
                    item.DateUpdate = DateTime.Now;
                    arr.Remove(d);
                }
                _customerService.UpdateContract(rs, arr);
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
                    Message = ResultStatus.Fail,
                }) ;
            }
        }
        [HttpGet]
        public IActionResult DeleteContract(long id)
        {
            try
            {
                var rs = _customerService.DeleteContract(id);
                return Json(new
                {
                    Result = true,
                    Message = ResultStatus.Success
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = false,
                    Mesage = ResultStatus.Fail
                });
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult ReadFile(long id, string img)
        {
            var rs = JsonHelper.DeserializeObject<List<UploadFileApiResult>>(_customerService.GetContractById(id).Files).FirstOrDefault(x => x.Url.Contains(img));
            //string wordHTML = System.IO.File.ReadAllText(rs.Url);
            //FileStream fstream = new FileStream("wwwroot/"+rs.Url, FileMode.Open, FileAccess.Read);
            //StreamReader sreader = new StreamReader(fstream, System.Text.Encoding.UTF8);
            //string sr = sreader.ReadToEnd();
            //ViewData["k"] = sr;
            FileStream fs = System.IO.File.OpenRead("wwwroot/" + rs.Url);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "123");
        }




        #endregion
        [HttpPost]
        public async Task<IActionResult> UploadFile(ICollection<IFormFile> files)
        {

            try
            {
                var listUpload = await UploadContractFiles(files,"Contract");

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
        public async Task<IActionResult> OnGetAsync(long id, string img)
        {

            var rs = JsonHelper.DeserializeObject<List<Document>>(_customerService.GetContractById(id).Files).FirstOrDefault(x => x.Url.Contains(img));
            if (rs == null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, rs, Operations.Read);

            if (authorizationResult.Succeeded)
            {
               var filepath = $"wwwroot/{rs.Url}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes, "application/msdownload", rs.FileName);
                
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }
        public  async Task<List<Document>> UploadContractFiles(ICollection<IFormFile> files, string f = "UploadFiles")
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            string folder = $"{f}/{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}/";
            // full path to file in temp location
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                folder);

            bool folderExists = Directory.Exists(filePath);
            if (!folderExists)
                Directory.CreateDirectory(filePath);
            var listUpload = new List<Document>();
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
                        listUpload.Add(new Document
                        {
                            FileName = formFile.FileName,
                            Url = url,
                            Author = user.Result.UserName
                        });
                    }
                }
            }

            return listUpload;
        }
        private async Task SendmailToApproved(BusinessTripModel rs, string urldomain, string email)
        {
            var messageBodyCus = "";
            MessageBody(urldomain, out messageBodyCus);
            messageBodyCus = messageBodyCus.Replace("{0}", rs.Name)
                .Replace("{1}", rs.Customer)

                .Replace("{2}", rs.Timer)
                .Replace("{3}", rs.Address)
                .Replace("{4}", rs.Contact)
                .Replace("{5}", rs.Phone)
                .Replace("{6}", rs.Note)              ;
            await _emailSender.SendEmailAsync(email, "Mail thông báo tự động",
                messageBodyCus); // order --> send mail cho client service           
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
        public async Task<IActionResult> DownloadCustomer()
        {
            try
            {
                return await Download(@"customer.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", i =>
                {
                    var list = Mapper.Map<List<CustomerViewModel>>(_customerService.GetAllCustomer());
                    IRow row = i.CreateRow(0);
                    row.CreateCell(0).SetCellValue("Client - Brand");
                    row.CreateCell(1).SetCellValue("Pic");
                    row.CreateCell(2).SetCellValue("Category");
                    row.CreateCell(3).SetCellValue("Phone");
                    row.CreateCell(4).SetCellValue("Email");
                    row.CreateCell(5).SetCellValue("Contact Person");
                    row.CreateCell(6).SetCellValue("Position");
                    row.CreateCell(7).SetCellValue("Status");
                    row.CreateCell(8).SetCellValue("INTERACTION (last day)");
                    row.CreateCell(9).SetCellValue("THE DAY SHOULD CALL");
                    foreach (var item in list)
                    {
                        row = i.CreateRow(list.IndexOf(item) + 1);
                        row.CreateCell(0).SetCellValue(checkNull(item.Name));
                        row.CreateCell(1).SetCellValue(checkNull(item.Pic));
                        row.CreateCell(2).SetCellValue(checkNull(item.Type));
                        row.CreateCell(3).SetCellValue(checkNull(item.Phone));
                        row.CreateCell(4).SetCellValue(checkNull(item.Email));
                        row.CreateCell(5).SetCellValue(checkNull(item.Contact));
                        row.CreateCell(6).SetCellValue(checkNull(item.Position));
                        row.CreateCell(7).SetCellValue(checkNull(item.Status));
                        row.CreateCell(8).SetCellValue(item.DateUpdated!=null? item.DateUpdated.Value.ToString("dd/MM/yyyy"):"");
                        row.CreateCell(9).SetCellValue(item.DayCall!=null? item.DayCall.Value.ToString("dd/MM/yyyy"):"");
                    }
                });
            }
            catch (Exception ex)
            {

                throw;
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
        private string checkNull(string i)
        {
            return !string.IsNullOrEmpty(i)? i : "";
        }
    }
}