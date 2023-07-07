using AllUp2.Models;

namespace AllUp2.ViewModels.Home
{
    public class BioVM
    {
        public List<Bio> Bios { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Service> Services { get; set; }
        public List<Icon> Icons { get; set; }

        public BioVM()
        {
            Bios = new List<Bio>();
            Brands = new List<Brand>();
            Services = new List<Service>();
            Icons = new List<Icon>();
        }
    }
}
