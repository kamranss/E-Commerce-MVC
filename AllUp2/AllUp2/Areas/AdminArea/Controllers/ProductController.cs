using AllUp2.Areas.AdminArea.Services.ProductS;
using AllUp2.DAL;
using AllUp2.Helper.FileExten;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Controllers
{
    
    [Area("AdminArea")]
    
    public class ProductController : Controller
    {
        private readonly IProService _proService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(IProService proService, IWebHostEnvironment webHostEnvironment)
        {
            _proService = proService;
            _webHostEnvironment = webHostEnvironment;
           
        }

        public IActionResult Index(int page, int take = 3)
        {
            var products = _proService.GetProducts(page, take);
            return View(products);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();

             var product =  _proService.Deatil(id);

            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            List<Category> categories = _proService.GetCategories();
            IEnumerable<SelectListItem> selectListItems = categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.Categories = selectListItems;
            //ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "Name");
           
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductCreateVM? productCreateVM)
        {
            ViewBag.Categories = _proService.GetCategories();

            if (!ModelState.IsValid) return View();
            Product product = new();
            foreach (var item in productCreateVM.Images)
            {

                if (!item.CheckFileType())
                {
                    ModelState.AddModelError("Photo", "Sehf");
                }
                if (item.CheckFileLenght(1000))
                {
                    ModelState.AddModelError("Photo", "Olcu boyukdur");
                }
                Image image = new Image();
                if (item == productCreateVM.Images[0])
                {
                    image.IsMain = true;
                }

                image.ImageUrl = item.SaveImage(_webHostEnvironment, "assets/images");
                //product.Images = new List<Image>();
                product.Images.Add(image);
            }
            product.Name = productCreateVM.Name;
            product.Price = productCreateVM.Price;
            product.Count = productCreateVM.Count;
            product.CategoryId = productCreateVM.CategoryId;
            product.CreationDate = DateTime.Now;

            _proService.Create(product);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int? id)
        {
            var selectListItems = _proService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name // Replace with the appropriate property of Category representing the display text
            });
            ViewBag.Categories = selectListItems;
            if (id == null) { return NotFound(); }

           var product = _proService.FindProduct(id);

            if (product == null) { return NotFound(); }

           var productUpdateVM = _proService.MapProducTotVM(product);
           return View(productUpdateVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(ProductUpdateVM productUpdateVM)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "Name");
                ViewBag.Categories = _proService.GetCategories();
                return View(productUpdateVM);
            }

            var product = _proService.FindProduct(productUpdateVM.Id);

            if (product == null) { return NotFound(); }

            if (productUpdateVM.NewImages != null)
            {
                foreach (var newImage in productUpdateVM.NewImages)
                {
                    if (!newImage.CheckFileType())
                    {
                        ModelState.AddModelError("NewImages", "Choose a picture");
                        return View(productUpdateVM);
                    }
                    if (newImage.CheckFileLenght(1000))
                    {
                        ModelState.AddModelError("NewImages", "Big size");
                        return View(productUpdateVM);
                    }

                    //Image image = new();
                    //image.ImageUrl = newImage.SaveImage(_webHostEnvironment, "img");
                    //product.Images.Add(image);

                    _proService.SaveProImage(newImage, product);
                }
            }

            product.Name = productUpdateVM.Name;
            product.Price = productUpdateVM.Price;
            product.Count = productUpdateVM.Count;
            product.CategoryId = productUpdateVM.CategoryId;

            _proService.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = _proService.IsProductExist(id);
            if (product == null) return NotFound();
            product.IsDeleted = true;
            _proService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult RemoveImage(int? id)
        {
            var image = _proService.FindImage(id);
            if (image == null)
            {
                return NotFound();
            }

           _proService.RemoveProImage(image);

            return RedirectToAction(nameof(Update), new { id = image.ProductId });
        }

    }
}
