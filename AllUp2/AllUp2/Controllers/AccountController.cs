using AllUp2.Helper;
using AllUp2.Models;
using AllUp2.Services.EmailService;
using AllUp2.Services.FileService;
using AllUp2.Services.OTPService;
using AllUp2.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace AllUp2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // this object works as AbDbContext but we use this one onlu for User based operations -- if we want to use only one user role we should use this object 
        private readonly SignInManager<AppUser> _signInManager; // this is serving for adding user to the session and removing user from session
        private readonly RoleManager<IdentityRole> _roleManager; // this object manages roles comes with .net package we can create our custom class instead od IdentityRole -- here we are working with all roles 
        private readonly IEmailService _emailService; // this service created by me within service layer for email vertification
        private readonly IFileService _fileService; // this service also created by me for working with files
        //private readonly OtpService _otpService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IFileService fileService, OtpService otpService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _fileService = fileService;
            //_otpService = otpService;
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) // model state erros occures because of the data anotation which we used within vm class
            {
                return View();
            }
            string otp = OtpService.GenerateOTP();
            AppUser user = new AppUser();
            user.Email = registerVM.Email;
            user.FullName = registerVM.FullName;
            user.UserName = registerVM.UserName;
            user.ConnectionId = null;
            user.OTP = otp;
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password); // this method will rsturn error which we indicated within startup class if nedeed
            if (!result.Succeeded)
            {
                // after passing validation it will try to create user if startup validation not passed will provide errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", errorMessage: error.Description); // there afre different type of errors thats why we will not specifically indicate one error
                }
                return View(registerVM);
            }
            //var userToUpdate = await _userManager.FindByIdAsync(userId);
            //userToUpdate.ConnectionId = "newConnectionId";
            //await userManager.UpdateAsync(userToUpdate);
            await _userManager.AddToRoleAsync(user, RoleEnums.Member.ToString());


            // Send Email vertification
            string body = string.Empty;
            string path = "wwwroot/templates/verify.html";
            string subject = "hey you verify your email!";

            body = _fileService.ReadFile(path, body);
            body = body.Replace("{{otp}}", otp);
            body = body.Replace("{{Fullname}}", user.FullName);

            _emailService.Send(user.Email, subject, body);

            //return RedirectToAction("index", "home", new {area="AdminArea"}); // if we want redirect to another area we should do like that 
            //return RedirectToAction("index", "home");

            return RedirectToAction(nameof(VertifyEmail), new { Email = user.Email }); // we are sending Email within the anonim object to the action 
        }
        public IActionResult VertifyEmail(string email)
        {
            ConfirmAccountVM confirmAccountVM = new ConfirmAccountVM()
            {
                Email = email
            };

            return View(confirmAccountVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(ConfirmAccountVM confirmAccountVM)
        {
            AppUser existUser = await _userManager.FindByEmailAsync(confirmAccountVM.Email);

            if (existUser.OTP != confirmAccountVM.OTP || string.IsNullOrEmpty(confirmAccountVM.OTP))
            {
                TempData["Error"] = "Wrong OTP";
                return RedirectToAction(nameof(VertifyEmail), new { Email = confirmAccountVM.Email });
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(existUser);
            await _userManager.ConfirmEmailAsync(existUser, token);

            await _signInManager.SignInAsync(existUser, false);

            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendOTP(string email) // this method serves for generating new OTP and sending this otp to user email
        {
            string otp = OtpService.GenerateOTP();
            AppUser existUser = await _userManager.FindByEmailAsync(email);
            existUser.OTP = otp; // we are renewing previous Otp
            await _userManager.UpdateAsync(existUser);
            string body = string.Empty;
            string path = "wwwroot/templates/verify.html";
            string subject = "hey you verify your email!";

            body = _fileService.ReadFile(path, body);
            body = body.Replace("{{otp}}", otp);
            body = body.Replace("{{fullname}}", existUser.FullName);

            _emailService.Send(existUser.Email, subject, body);

            //return RedirectToAction("index", "home", new {area="AdminArea"}); // if we want redirect to another area we should do like that 
            //return RedirectToAction("index", "home");

            return RedirectToAction(nameof(VertifyEmail), new { Email = existUser.Email }); // we are sending Email within the anonim object to the action 

        }

        public async Task<IActionResult> ChangePassowrd(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name); // finding user referring to session
            IdentityResult result = await _userManager.ChangePasswordAsync(existUser, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);

            if (result.Succeeded)
            {
                ViewBag.IsSuccess = true;
                return View(changePasswordVM);
            }
            else
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View(changePasswordVM);
            }
        }
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string? ReturnUrl)
        {
            if (!ModelState.IsValid) return View("logn", loginVM);
            // here we are checking whether username or email exist in db 
            AppUser user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            //if (user == null)
            //{
            //    user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            //}
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Email is wrong...");
                return View("login", loginVM);
            }

            if (user.IsActive)
            {
                ModelState.AddModelError("", "Your account Disabled...");
                return View(loginVM);
            }


            // here we are checking whether password is valid 
            //await _signInManager.PasswordSignInAsync(user, loginVM.Password, isPersistent:false);
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);// method takes 4 parameter 1 user,2 password, 3 RememberMe, 4 Lockout
            if (result.IsLockedOut) // checking attempt number 
            {
                ModelState.AddModelError("", "Your account has been blocked...");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Email is wrong...");
                return View(loginVM);
            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            // checking user role
            //await _userManager.GetUsersInRoleAsync(RoleEnums.Admin.ToString();
            await _signInManager.SignInAsync(user, loginVM.RememberMe); // keeps user in session

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(RoleEnums.Admin.ToString()))
            {
                return RedirectToAction("index", "dashboard", new { area = "adminarea" });
            }

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddRole()
        {

            foreach (var item in Enum.GetValues(typeof(RoleEnums)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            return Content("added");
        }
   
    }
}
