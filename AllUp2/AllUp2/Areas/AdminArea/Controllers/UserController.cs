using AllUp2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string name)
        {
            var users = name != null ? _userManager.Users.Where(u => u.FullName.ToLower().Contains(name.ToLower())).ToList() :
                _userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult> BlockOrActive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);   
            return RedirectToAction(nameof(Index));
        }
    }
}

