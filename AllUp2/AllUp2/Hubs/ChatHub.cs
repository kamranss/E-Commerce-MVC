using AllUp2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace AllUp2.Hubs
{
    public class ChatHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        // we will find connected user and assign connection id to this user
        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated) // this comes with Hub class
            {
                var user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result; // the result is used to avoid the async signature like await etc. it is changing behaviour to synchronously
                user.ConnectionId = Context.ConnectionId;
                var result = _userManager.UpdateAsync(user).Result;
                Clients.All.SendAsync("userConnect", user.Id, result);// tis is calling another method on client side
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // when this method called within the login action  await _signInManager.SignInAsync(user, loginVM.RememberMe);  it captures all user information  and after Context retriving this information from it
            if (Context.User.Identity.IsAuthenticated) // this comes with Hub class -- basically Context class retrieves user info when users log in referring to the session token and etc 
            {
                var user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result; // the result is used to avoid the async signature like await etc. it is changing behaviour to synchronously
                user.ConnectionId = null;
                var result = _userManager.UpdateAsync(user).Result;
                Clients.All.SendAsync("userDisconnect", user.Id, result);// tis is calling another method on client side
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
