using AllUp2.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Services.LoginService
{
    public interface ILoginService
    {
        public void Login(LoginVM loginVM, string? ReturnUrl);
    }
}
