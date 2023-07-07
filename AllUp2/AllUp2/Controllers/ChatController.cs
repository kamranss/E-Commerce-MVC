using AllUp2.Hubs;
using AllUp2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AllUp2.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext; // by using this approach we do not need create additional class we can call this object and use its features

        public ChatController(UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext)
        {
            this._userManager = userManager;
            _hubContext = hubContext;
        }

        public IActionResult Chat()
        {
            //_hubContext.Clients.AllExcept(""); // All clients except one
            //_hubContext.Clients.Client(""); // just one client
            //_hubContext.Clients.Clients(""); // this is all clients

            var query = _userManager.Users.AsQueryable();
            ViewBag.Users = query.ToList();
            return View();
        }
        public async Task<IActionResult> ShowAlert(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _hubContext.Clients.Client(user.ConnectionId).SendAsync("Show alert", user.FullName);
            return Content("sended");
        }
    }
}
