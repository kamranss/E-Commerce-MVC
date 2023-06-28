using AllUp2.Helper;
using AllUp2.Models;
using AllUp2.Services.EmailService;
using AllUp2.Services.FileService;
using AllUp2.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // this object works as AbDbContext but we use this one onlu for User based operations -- if we want to use only one user role we should use this object 
        private readonly SignInManager<AppUser> _signInManager; // this is serving for adding user to the session and removing user from session
        private readonly RoleManager<IdentityRole> _roleManager; // this object manages roles comes with .net package we can create our custom class instead od IdentityRole -- here we are working with all roles 
        private readonly IEmailService _emailService; // this service created by me within service layer for email vertification
        private readonly IFileService _fileService; // this service also created by me for working with files

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _fileService = fileService;
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
            string otp = GenerateOTP();
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

            return View();
        }
        public async Task<IActionResult> AddRole()
        {
            //if (await _roleManager.RoleExistsAsync("Admin)")
            //{
            //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            //}


            //await  _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            //await  _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            //await  _roleManager.CreateAsync(new IdentityRole { Name = "Member" });


            foreach (var item in Enum.GetValues(typeof(RoleEnums)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            return Content("added");
        }
        private static string GenerateOTP()
        {
            Random random = new Random();
            int otpnumber = random.Next(100000, 999999); // we will give squence and it will provide random number -- length will be 6
            return otpnumber.ToString();
        }
    }
}
