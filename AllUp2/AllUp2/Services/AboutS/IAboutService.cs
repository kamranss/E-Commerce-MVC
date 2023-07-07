using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Category;
using AllUp2.ViewModels.Home;

namespace AllUp2.Services.AboutS
{
    public interface IAboutService
    {
        About GeAboutInfo();
        public AboutVM MapAboutInfoToVM(About aboutInfo);


    }
}
