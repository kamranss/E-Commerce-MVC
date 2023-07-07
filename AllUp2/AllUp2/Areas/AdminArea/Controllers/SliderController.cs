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
            Slider slider = new Slider();
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

                slider.ImageUrl = item.SaveImage(_webHostEnvironment, "assets/images"); // this method extendet to the IFromFile object

            }
            slider.Name = sliderCreateVM.Name;
            slider.Description = sliderCreateVM.Description;
            slider.Suggestion = sliderCreateVM.Suggestion;
            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(c => c.Id == id);
            if (slider == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", slider.ImageUrl); // this methods creates path 
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


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = _appDbContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            var sliderUpdateVM = new SliderCreateVM
            {
                Photo = new IFormFile[] { null }
            };
            return View(sliderUpdateVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(SliderCreateVM sliderCreateVM, int? id)
        {
            if (ModelState.IsValid)
            {
                var slider = _appDbContext.Sliders.FirstOrDefault(s => s.Id == id);
                if (slider == null)
                {
                    return NotFound();
                }

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", slider.ImageUrl);
                FileExtension.DeleteFile(path);

                var newPictures = sliderCreateVM.Photo;
                if (newPictures == null || newPictures.Length == 0)
                {
                    ModelState.AddModelError("Pictures", "Add a picture");
                    return View(sliderCreateVM);
                }

                foreach (var picture in newPictures)
                {
                    if (picture == null)
                    {
                        ModelState.AddModelError("Pictures", "Add a picture");
                        return View(sliderCreateVM);
                    }

                    if (!picture.CheckFileType())
                    {
                        ModelState.AddModelError("Pictures", "Choose a picture");
                        return View(sliderCreateVM);
                    }

                    if (picture.CheckFileLenght(1000))
                    {
                        ModelState.AddModelError("Pictures", "Big size");
                        return View(sliderCreateVM);
                    }

                    slider.ImageUrl = picture.SaveImage(_webHostEnvironment, "img");
                }

                _appDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(sliderCreateVM);
        }
    }
}
