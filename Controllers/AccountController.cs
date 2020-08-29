using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LienPhatERP.Models;
using LienPhatERP.Models.AccountViewModels;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using MimeKit;
using LienPhatERP.Helper;
using LienPhatERP.Entities;
using LienPhatERP.Contants;
using Microsoft.AspNetCore.Http;

namespace LienPhatERP.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IMemberService _memberService;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICommonService _commonService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IMemberService memberService,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IHostingEnvironment env,
            ILogger<AccountController> logger,
            IAuthorizationService authorizationService,
            ICommonService commonService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _memberService = memberService;
            _configuration = configuration;
            _env = env;
            _authorizationService = authorizationService;
            _commonService= commonService;
        }

        [TempData] public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
           await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = _userManager.Users.Where(u => u.UserName == model.Email).SingleOrDefault();
            if (user != null)
            {
                if (user.LockoutEnabled)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //   var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                   
                   
                    _logger.LogInformation("User logged in.");
                    //  var token = GenerateJwtToken(model.Email, appUser);
                    return RedirectToLocal(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ApiLogin([FromBody]ApiLoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var appUser = _memberService.GetUserRolebyEmail(model.Email);
                 
              
                return Ok(new {appUser.User.Name,appUser.User.Address
                    ,appUser.User.PhoneNumber,RoleName=appUser.Role.Name});
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe,
            string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result =
                await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe,
                    model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model,
            string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
     //   [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, IsManager = model.IsManager, Name = model.Name ,LockoutEnabled = false};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if (model.RoleId != null)
                    {
                        user = await _userManager.FindByEmailAsync(model.Email);
                        var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    //   var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //  var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //    var messageBody = Sendmail(model, callbackUrl);
                    //     await _emailSender.SendEmailAsync(model.Email, "tst", messageBody);
                    //    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction(nameof(ResetOk), new { message="Tạo user thành công", url= "Register" });
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(Login), "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model,
            string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user= await _userManager.FindByNameAsync(model.Email);
                user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null||(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                    $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string name = null)
        {
            if (name == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }

            var model = new ResetPasswordViewModel { Email = name };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            ViewData["Title"] = "Đổi Mật Khẩu";
            string message = "Đã bị lỗi";
            string url = "/Home/Dashboard";
            if (!ModelState.IsValid)
            {
                return View(model); /*ResetPasswordConfirmation*/
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                message = "Đổi Mật Khẩu Không Thành Công";
                url = "ResetPassword";
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetOk), new { message, url }); /*ResetPasswordConfirmation*/

            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
            if (result.Succeeded)
            {
                message = "Đổi Mật Khẩu Thành Công";
                return RedirectToAction(nameof(ResetOk), new { message, url }); /*ResetPasswordConfirmation*/
            }

            AddErrors(result);
           // url = $"ResetPassword?name={user.UserName}";
            return View(model);
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordForUser(ResetPasswordViewModel model)
        {
            ViewData["Title"] = "Đổi Mật Khẩu";
            string message = "Đã bị lỗi";
            string url = "/Account/GetAllUser";
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ResetOk), new { message, url }); /*ResetPasswordConfirmation*/
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                message = "Đổi Mật Khẩu Không Thành Công";
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetOk), new { message, url }); /*ResetPasswordConfirmation*/

            }
            var code= await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user,code,model.Password);
            if (result.Succeeded)
            {
                message = "Đổi Mật Khẩu Thành Công";
                return RedirectToAction(nameof(ResetOk), new { message, url }); /*ResetPasswordConfirmation*/
            }

            AddErrors(result);
            return RedirectToAction(nameof(ResetOk), new { message, url });
            //return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetOk(string message, string url)
        {
            ViewData["Result"] = message;
            ViewData["Action"] = url;
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult GetAllUser()
        {

            var user = _memberService.GetAllUser();

            var result = Mapper.Map<List<MemberViewModel>>(user);
            return View(result);
        }

        [HttpGet]
        public IActionResult GetRoles()
        {

            return View();
        }
        /// <summary>
        /// Hàm lấy dữ liệu Role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DataJsonRoles()
        {
            var role = _roleManager.Roles.ToList();
            var Result = Mapper.Map<List<RoleViewModel>>(role);
            return Json(new
            {
                result = Result,
            });

        }
        /// <summary>
        /// Hàm  thêm Role mới
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRole(string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                //await _roleManager.CreateAsync()
                await _roleManager.CreateAsync(new IdentityRole() { Name = role });
            }

            // ApplicationUser user = await _userManager.FindByEmailAsync("nvthintt@gmail.com");

            //if (!User.IsInRole("Admin"))
            //{
            //    await _userManager.AddToRoleAsync(user, "Admin");
            //}
            //if (!User.IsInRole("blah"))
            //{
            //    await _userManager.AddToRolesAsync(user, new string[] { "blah", "Test" });
            //}

            return Redirect("GetRoles");
        }
        /// <summary>
        /// MyProfile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MyProfile()
        {
            var rs = _userManager.FindByNameAsync(User.Identity.Name);
            return View(new RegisterModel(rs.Result));
        }

        [HttpGet]
        public IActionResult MyProfileAdmin(string id = "")
        {
            var rs = _userManager.FindByNameAsync(id).Result;
            if (rs!=null)
            {
                return View(new RegisterModel(rs));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> MyProfileAdmin([FromForm] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.UserName).Result;
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Salary = model.Salary;
                    user.IdentityCard = model.IdentityCard;
                    user.IssuedBy = model.IssuedBy;
                    user.IssuedDate = model.IssuedDate;
                    user.Level = model.Level;
                    user.GraduationYear = model.GraduationYear;
                    user.Male = model.Male;
                    user.School = model.School;
                    user.Folk = model.Folk;
                    user.DateWork = model.DateWork;
                    user.Position = model.Position;
                    //if (model.File != null)
                    //{
                    //    var img = await UploadFileImage(model.File);
                    //    if (!string.IsNullOrEmpty(img))
                    //    {
                    //        string full = Path.Combine(_env.WebRootPath, user.Image);
                    //        if (System.IO.File.Exists(full))
                    //            System.IO.File.Delete(full);
                    //        user.Image = model.Image;
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(model.Files) && !user.Files.Equals(model.Files))
                    {
                        var f = model.Files.Replace("[", "");
                        if (!string.IsNullOrEmpty(user.Files))
                            user.Files = user.Files.Replace("]", "," + f);
                        else
                            user.Files = model.Files;
                    }
                    user.NoContract = model.NoContract;
                    user.NoBHXH = model.NoBHXH;
                    user.CurrentContract = user.CurrentContract;
                    user.Date = model.Date;
                    user.UserCode = model.UserCode;
                    user.ContractType = model.ContractType;
                    user.TGHD = model.TGHD;
                    user.TGBH = model.TGBH;
                    user.Bank = model.Bank;
                    user.Matrimony = model.Matrimony;
                    user.Home = model.Home;
                    user.Address = model.Address;
                    user.Province = model.Province;
                    user.District = model.District;
                    user.CompanyName = model.CompanyName;
                    user.PhoneNumber = model.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User update a account.");

                        return View(model);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> MyProfile([FromForm] RegisterModel model)      
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.UserName).Result;
                if (user != null)
                {
                    user.Name = model.Name;
                    user.IdentityCard = model.IdentityCard;
                    user.IssuedBy = model.IssuedBy;
                    user.IssuedDate = model.IssuedDate;
                    user.Level = model.Level;
                    user.GraduationYear = model.GraduationYear;
                    user.Male = model.Male;
                    user.School = model.School;
                    user.Folk = model.Folk;
                    user.DateWork = model.DateWork;
                    user.Position = model.Position;
                    //if (model.File!= null)
                    //{
                    //    var img= await UploadFileImage(model.File);
                    //    user.Image = img;
                        
                    //    if (!string.IsNullOrEmpty(img))
                    //    {
                    //        string full = Path.Combine(_env.WebRootPath, user.Image);
                    //        if (System.IO.File.Exists(full))
                    //            System.IO.File.Delete(full);
                    //        user.Image = model.Image;
                    //    }
                    //}
                    user.Date = model.Date;
                    user.Matrimony = model.Matrimony;
                    user.Home = model.Home;
                    user.Address = model.Address;
                    user.Province = model.Province;
                    user.District = model.District;
                    user.CompanyName = model.CompanyName;
                    user.PhoneNumber = model.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User update a account.");

                        return RedirectToAction("MyProfile");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeImage([FromForm] ChangeImageModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.Id).Result;
                if (user != null)
                {
                   
                    if (model.File != null)
                    {
                        var img = await UploadFileImage(model.File);
                      

                        if (!string.IsNullOrEmpty(img))
                        {
                            string full = Path.Combine(_env.WebRootPath, img);
                            if (System.IO.File.Exists(full))
                                System.IO.File.Delete(full);
                            user.Image = img;
                        }
                    }
                  
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User update a account.");

                        return Ok(user.Image);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return  RedirectToAction("MyProfile");
        }

        [HttpPost]
        public async Task<string> UploadFileImage(IFormFile file)
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

                return $"/{listUpload.Url}";
            }
            catch (Exception exp)
            {

                _logger.LogCritical($"Exception generated when uploading file - {exp.Message} ");
                string message = $"file / upload failed!";
                return "";
            }
        }
        [HttpGet]
        public async Task<IActionResult> OnGetAsync(string username, string img)
        {

            var rs = JsonHelper.DeserializeObject<List<Document>>(_userManager.FindByNameAsync(username).Result.Files).FirstOrDefault(x => x.Url.Contains(img));
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

        [HttpPost]
        public async Task<IActionResult> InsertRegisterDateOff(RegisterDateOffModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                model.UserCreate = user.Id;
                var date = model.DateOffNumber;
                if (model.DateOffNumber > 0)
                {

                    model.DateOffNumber = model.ToDateOff.AddDays(1).Subtract(model.FromDateOff).Days ;
                    var subDay = 0;
                    for (int i = 0; i < model.DateOffNumber; i++)
                    {
                        if (model.FromDateOff.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        {
                            subDay += 1;
                        }
                    }
                    model.DateOffNumber = model.ToDateOff.AddDays(1).Subtract(model.FromDateOff).Days -
                                          date - subDay;
                }
                else
                {
                   model.DateOffNumber = model.ToDateOff.AddDays(1).Subtract(model.FromDateOff).Days;
                    var subDay = 0;
                    for (int i = 0; i < model.DateOffNumber; i++)
                    {
                        if (model.FromDateOff.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        {
                            subDay += 1;
                        }
                    }
                    model.DateOffNumber = model.ToDateOff.AddDays(1).Subtract(model.FromDateOff).Days -subDay;
                }
           
                if (model.Type.Equals("1"))
                {
                    model.DateOffNumber = 0.5f;
                }
                if (model.Type.Equals("2"))
                {
                    model.DateOffNumber = 1f;
                    
                }
                var rs = _commonService.InsertRegisterDateOff(Mapper.Map<RegisterDateOff>(model));
                var urldomain = Request.Scheme + "://" + Request.Host.Value;
                if (user.IsManager)
                {
                    await SendmailToApproved(rs, urldomain, _configuration["Email:Boss"]);
                }
                var email = _commonService.GetEmailUser(model.Department);            
                foreach (var item in email)
                {
                    await SendmailToApproved(rs, urldomain, item.Email);
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

        private async Task SendmailToApproved(RegisterDateOff rs, string urldomain, string email)
        {
            var messageBodyCus = "";
            MessageBody("NotifyDateoff.html", out messageBodyCus);
            messageBodyCus = messageBodyCus.Replace("{0}", rs.Name)
                .Replace("{1}", rs.DateOffNumber.ToString())

                .Replace("{2}", rs.FromDateOff.ToString("dd/MM/yyyy"))
                .Replace("{3}", rs.ToDateOff.ToString("dd/MM/yyyy"))
                .Replace("{4}", rs.Reason)
                .Replace("{5}",
                    $"{urldomain}/Account/UpdateRegisterDateOff?id={rs.Token}");
            //MessageBody("RegisterDateOff.html", out messageBodyCus);
            //messageBodyCus = messageBodyCus.Replace("{1}", rs.Name)
            //    .Replace("{2}", rs.DateOffNumber.ToString())
            //    .Replace("{3}",
            //        $"{rs.FromDateOff.Day.ToString()} tháng {rs.FromDateOff.Month.ToString()} năm {rs.FromDateOff.Year}")
            //    .Replace("{4}", $"{rs.ToDateOff.Day.ToString()} tháng {rs.ToDateOff.Month.ToString()} năm {rs.ToDateOff.Year}")
            //    .Replace("{5}", rs.Reason)
            //    .Replace("{6}",
            //        $"{urldomain}/Account/UpdateRegisterDateOff?id={rs.Token}")
            //    .Replace("{7}",
            //        $"{urldomain}/Account/RejectRegisterDateOffManager?id={rs.Token}");
            await _emailSender.SendEmailAsync(email, "Mail thông báo tự động",
                messageBodyCus); // order --> send mail cho client service           
        }

        [HttpGet]
        public IActionResult UpdateRegisterDateOff(string id)
        {
            var rs = _commonService.GetRegisterById(id);
            if (rs.Status.Equals("0"))
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                rs.Status = "1";
                rs.UserApproved = user.Id;
                _commonService.UpdateRegisterDateOff(rs);
                ViewData["Result"] = "Duyệt thành công";
            }
            else
            {
                ViewData["Result"] = "Bạn đã duyệt rồi";
            }
            
        
            //var urldomain = Request.Scheme + "://" + Request.Host.Value;
            //var messageBodyCus = "";
            //MessageBody("RegisterDateoff.html", out messageBodyCus);
            //messageBodyCus = messageBodyCus.Replace("{0}", user.Name)
            //    .Replace("{1}", rs.Name)
            //    .Replace("{2}", rs.DateOffNumber.ToString())
            //    .Replace("{3}", $"{rs.FromDateOff.Day.ToString()} tháng {rs.FromDateOff.Month.ToString()} năm {rs.FromDateOff.Year}")
            //    .Replace("{4}", $"{rs.ToDateOff.Day.ToString()} tháng {rs.ToDateOff.Month.ToString()} năm {rs.ToDateOff.Year}")
            //    .Replace("{5}", rs.Reason)
            //    .Replace("{6}", $"<div style='-moz-border-radius: 30px; -webkit-border-radius: 30px; border-radius: 30px; width: 200px; height: 50px; background-color:#716aca; display: table-cell;'><a href = '{urldomain}/Account/UpdateRegisterDateOffManager?id={rs.Token}&reson=' style='width:220; display:block; text-decoration:none; border:0; text-align:center; font-weight:bold;font-size:18px; font-family: Arial, sans-serif; color: #ffffff;padding-top: 16px;'class='button_link'>Đồng ý</a></div><div style='-moz-border-radius: 30px; -webkit-border-radius: 30px; border-radius: 30px; width: 200px;height:50px;background-color:#716aca; display: table-cell;'><a href='#' style='width:220; display:block; text-decoration:none; border:0; text-align:center; font-weight:bold;font-size:18px; font-family: Arial, sans-serif; color: #ffffff;padding-top: 16px;'class='button_link' data-dismiss='modal' data-toggle='modal' data-target='#myModalRegister' data-code=' data-code2=''>Từ chối</a></div>");
            //ViewData["html"] = messageBodyCus;
            //ViewData["k"] = id;

            return View(rs);
        }
        [HttpGet]
        public IActionResult DeleteRegisterDateOff(string id)
        {
            try
            {
                _commonService.DeleteRegisterDateOff(id);
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
                    Result= false,
                    Message= ResultStatus.Fail
                });
            }
        }
        [HttpGet]
        public IActionResult DeleteRegisterDateOffAdmin(string id)
        {
            try
            {
                _commonService.DeleteRegisterDateOffAdmin(id);
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
        public IActionResult RejectRegisterDateOffManager(string id,string reson = "")
        {
            var rs = _commonService.GetRegisterById(id);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            rs.UserApproved = user.Id;
            rs.Refuse = reson;
            rs.Status = "1";
            if (!string.IsNullOrEmpty(reson))
                rs.Status = "2";
            _commonService.UpdateRegisterDateOff(rs);
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Hrm")]
        public IActionResult GetDataAllRegisterDateOff(string startDate, string endDate, string department, string key)
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
                var rs1 = _commonService.GetAllRegisterDateOff(f, d, department, key);
                return View(rs1);

            }
            if (user.Result.IsManager)
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (role != null)
                {
                    var rs = _commonService.GetAllRegisterDateOff(f, d, role.Value, key);
                    ViewData["1"] = role.Value;
                    return View(rs);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult GetDataAllRegisterDateOffManager(string startDate, string endDate, string key)
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
                    var rs = _commonService.GetAllRegisterDateOffAdmin(f, d, role.Value, key);
                    return View(rs);
                }
            }
            return NotFound();
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
        /// <summary>
        /// Hàm remove Role
        /// Kiểm tra Id != null và kiểm tra role != null
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> RemoveRole(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                //var claims = await _roleManager.GetClaimsAsync(new IdentityRole() { Name = role });
                //var someClaim = claims.FirstOrDefault(x => x.Type == "Claim"
                var role = await _roleManager.FindByIdAsync(Id);
                if (role != null)
                {
                    //await _roleManager.CreateAsync()
                    await _roleManager.DeleteAsync(role);
                    return Json(new
                    {
                        Result = true,
                        Message = "Xóa câu hỏi thành công"
                    });

                }

                return Json(new
                {
                    Result = false,
                    Message = "Xóa câu hỏi không thành công"
                });
            }
            //"Xóa câu hỏi không thành công"
            // ApplicationUser user = await _userManager.FindByEmailAsync("nvthintt@gmail.com");

            //if (!User.IsInRole("Admin"))
            //{
            //    await _userManager.AddToRoleAsync(user, "Admin");
            //}
            //if (!User.IsInRole("blah"))
            //{
            //    await _userManager.AddToRolesAsync(user, new string[] { "blah", "Test" });
            //}

            return Json(new
            {
                Result = true,
                Message = "Đã bị lỗi"
            });

        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        //private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new
        //        {
        //            Result = false,
        //            Message = "Đã bị lỗi"
        //        });
        //    }
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        //    var token = new JwtSecurityToken(
        //        _configuration["JwtIssuer"],
        //        _configuration["JwtIssuer"],
        //        claims,
        //        expires: expires,
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        private string Sendmail(RegisterViewModel model, string callbackUrl)
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "Templates"
                             + Path.DirectorySeparatorChar.ToString()
                             + "EmailTemplate"
                             + Path.DirectorySeparatorChar.ToString()
                             + "Register_EmailTemplate.html";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody,
                "Dang ky",
                String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                model.Email,
                model.Email,
                model.Password,
                "Test",
                callbackUrl
            );
            return messageBody;
        }
        public async Task<IActionResult> EditUser(UserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.IsManager = model.IsManager;
                if (model.RoleId != null)
                {
                    var roles = _userManager.GetRolesAsync(user);
                    var role = _roleManager.FindByIdAsync(model.RoleId.ToString());
                    foreach (var r in roles.Result)
                    {
                        await _userManager.RemoveFromRoleAsync(user, r);
                    }
                  
                    await _userManager.AddToRoleAsync(user, role.Result.Name);
                }

                await _userManager.UpdateAsync(user);
                return Json(new
                {
                    Result = true,
                    Message = "Thành công"
                });

            }
            return Json(new
            {
                Result = false,
                Message = "Đã bị lỗi"
            });
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            //}
            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    
    [HttpGet]
        public async Task<IActionResult> RemoveRoleUser(string userId, List<string> role)
        {
            ViewData["Title"] = "Xóa quyền user";
            string message = "Xóa quyền user không thành công";
            string url = "GetAllUser";
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ResetOk), new {message, url});
            }

            if (await _userManager.FindByIdAsync(userId) != null && role.Count() > 0)
            {
                foreach (var i in role)
                {
                    var r = _memberService.GetUserRoleById(i);
                    if (r != null)
                    {
                        _memberService.RemoveRole(r);
                    }
                }

                message = "Xóa quyền user thành công";
                return RedirectToAction(nameof(ResetOk), new {message, url});
            }

            return RedirectToAction(nameof(ResetOk), new {message, url});
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            ViewData["Title"] = "Xóa user";
            string message = "Xóa user không thành công";
            string url = "GetAllUser";
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ResetOk), new {message, url});
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByNameAsync(userId);
                if (user != null)
                {
                    await _userManager.SetLockoutEnabledAsync(user,true);
                    message = "Xóa user thành công";
                    return RedirectToAction(nameof(ResetOk), new {message, url});
                }

                return RedirectToAction(nameof(ResetOk), new {message, url});
            }

            return RedirectToAction(nameof(ResetOk), new {message, url});
        }
    }
}

#endregion

