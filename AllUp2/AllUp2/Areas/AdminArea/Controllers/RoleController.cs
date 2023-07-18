using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private IMemoryCache _memoryCach;

        public RoleController(AppDbContext appDbContext, IMemoryCache memoryCach, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _memoryCach = memoryCach;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName)) return BadRequest();
            if (await _roleManager.RoleExistsAsync(RoleName))
            {
                return BadRequest();
            }
            await _roleManager.CreateAsync(new IdentityRole { Name = RoleName });
            return RedirectToAction("index");
        }


        public async Task<IActionResult> Delete(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("index");

        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userroles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.ToList();


            RoleUpdateVM roleUpdateVM = new RoleUpdateVM()
            {
                User = user,
                UserRoles = userroles,
                Roles = roles
            };
            return View(roleUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string userId, List<string> newRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, newRoles);

            return RedirectToAction("index", "user");
        }
    }
}
