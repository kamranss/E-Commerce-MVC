using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Category;
using AllUp2.ViewModels.Home;

namespace AllUp2.Services.AboutS
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _appDbContext;

        public AboutService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public About GeAboutInfo()
        {
            var aboutInfo = _appDbContext.Abouts.FirstOrDefault();
            return aboutInfo;
        }

        public AboutVM MapAboutInfoToVM(About aboutInfo)
        {
            AboutVM abautVmInfo = new AboutVM();
            abautVmInfo.Name = aboutInfo.Name;
            abautVmInfo.Description = aboutInfo.Description;
            abautVmInfo.AboutTeam = aboutInfo.AboutTeam;
            abautVmInfo.AboutCompany = aboutInfo.AboutCompany;
            abautVmInfo.Testimonials = aboutInfo.Testimonials;
            abautVmInfo.ImageUrl = aboutInfo.ImageUrl;

            return abautVmInfo;

        }
       
    }
}
