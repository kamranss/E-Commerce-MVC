using AllUp2.Areas.AdminArea.Services.SliderS;
using AllUp2.DAL;
using AllUp2.Helper.FileExten;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISliderS _sliderService;

        public SliderController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, ISliderS sliderService)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _sliderService = sliderService;
        }
        public IActionResult Index(int page, int take = 3)
        {
            var sliders = _sliderService.GetSliders(page, take);
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SliderCreateVM sliderCreateVM)
        {
            foreach (var item in sliderCreateVM.Photo)
            {
                if (item.CheckIsFileUploaded()) //sliderCreateVM.Photo == null
                {
                    ModelState.AddModelError("Photo", "Bosh qoyma");
                    return View();
                }
                if (!item.CheckFileType())   //!sliderCreateVM.Photo.ContentType.Contains("image")
                {
                    ModelState.AddModelError("Photo", "Faylin tipi ferqlidir");
                    return View();
                }
                if (item.CheckFileLenght(1000)) //sliderCreateVM.Photo.Length<1000
                {
                    ModelState.AddModelError("Photo", "Faylin olcusu boyukdur");
                    return View();
                }
                /*string path2 = _webHostEnvironment.WebRootPath + @"\img\" + sliderCreateVM.Photo.FileName; */ // this alllows us to get out path to the rrot folder




                //string fileName = Guid.NewGuid() + sliderCreateVM.Photo.FileName; // this is generating new eandom name 
                //string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                //using (FileStream stream = new FileStream(path, FileMode.Create))
                //{
                //    sliderCreateVM.Photo.CopyTo(stream);
                //}


                Slider slider = new Slider();
                //slider.ImageUrl = fileName

                slider.ImageUrl = item.SaveImage(_webHostEnvironment, "img"); // this method extendet to the IFromFile object

                _appDbContext.Sliders.Add(slider);
                _appDbContext.SaveChanges();

            }
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(c => c.Id == id);
            if (slider == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.ImageUrl); // this methods creates path 
            //if (System.IO.File.Exists(path)) // checking whether path exist
            //{
            //    System.IO.File.Delete(path); // deleting file from the path
            //}
            FileExtension.DeleteFile(path);

            _appDbContext.Sliders.Remove(slider);
            int ruselt = _appDbContext.SaveChanges(true);
            if (ruselt > 0) { return RedirectToAction(nameof(Index)); }
            return NotFound();
        }
    }
}
