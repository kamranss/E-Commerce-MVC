using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.Services.PaymentS;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace AllUp2.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly PaymentService _paymentService;

        public CheckOutController(AppDbContext appDbContext, PaymentService paymentService)
        {
            _appDbContext = appDbContext;
            _paymentService = paymentService;
        }
        private string GetBasket()
        {
            string basket = Request.Cookies["basket"];
            return basket;
        }
        public IActionResult Index()
        {
            string basket = GetBasket();

            List<BasketVM> products;

            if (basket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (var item in products)
                {
                    Product existProduct = _appDbContext.Products
                        .Include(p => p.Images)
                        .FirstOrDefault(p => p.Id == item.Id);

                    item.Name = existProduct.Name;
                    item.Price = existProduct.Price;
                    item.ImageUrl = existProduct.Images.FirstOrDefault().ImageUrl;
                }
            }

            var checkoutVM = new CheckOutVM();
            checkoutVM.BasketItems = products;
            return View(checkoutVM);
        }

        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> PlaceOrder(CheckoutVM checkoutVM, IHubContext<NotificationHub> hubContext)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string fullName = $"{checkoutVM.FirstName} {checkoutVM.LastName}";

        //        string shippingAddress = CombineAddressFields(
        //            checkoutVM.AddressLine1,
        //            checkoutVM.AddressLine2,
        //            checkoutVM.City,
        //            checkoutVM.State,
        //            checkoutVM.ZipCode,
        //            checkoutVM.Country
        //        );

        //        var buyer = new Buyer
        //        {
        //            Name = fullName,
        //            Email = checkoutVM.Email,
        //            Phone = checkoutVM.Phone,
        //            BillingAddress = shippingAddress
        //        };

        //        var order = new Order
        //        {
        //            TotalAmount = checkoutVM.BasketItems.Sum(p => p.BasketCount * p.Price),
        //            OrderDate = DateTime.Now,
        //            Buyer = buyer,
        //            ShippingStatus = ShippingStatus.Accepted
        //        };

        //        foreach (var item in checkoutVM.BasketItems)
        //        {
        //            var orderItem = new OrderItem
        //            {
        //                ProductId = item.Id,
        //                Quantity = item.BasketCount,
        //                Price = item.Price,
        //                Tax = 0,
        //                Order = order
        //            };

        //            order.OrderItems.Add(orderItem);
        //        }

        //        _appDbContext.Buyers.Add(buyer);
        //        _appDbContext.Orders.Add(order);
        //        await _appDbContext.SaveChangesAsync();

        //        await hubContext.Clients.Users("Admin", "SalesManager").SendAsync("NewOrderPlaced", order.Id);

        //        return RedirectToAction("Payment", new { orderId = order.Id });
        //    }

        //    return View("Index", checkoutVM);
        //}


        //[HttpGet]
        //public async Task<IActionResult> Payment(int orderId)
        //{
        //    var order = await _appDbContext.Orders.FindAsync(orderId);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var paymentIntent = await _stripePaymentService.CreatePaymentIntent((decimal)order.TotalAmount, "usd");

        //    var paymentViewModel = new PaymentVM
        //    {
        //        OrderId = order.Id,
        //        ClientSecret = paymentIntent.ClientSecret
        //    };

        //    return View(paymentViewModel);
        //}

        //[HttpGet]
        //public IActionResult ThankYou(int orderId)
        //{
        //    var order = _appDbContext.Orders.FirstOrDefault(o => o.Id == orderId);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    order.PaymentStatus = PaymentStatus.Completed;
        //    _appDbContext.SaveChanges();

        //    return View();
        //}



        //[HttpGet]
        //public IActionResult PaymentFailure()
        //{
        //    return View();
        //}

        //private string CombineAddressFields(params string[] addressFields)
        //{
        //    string separator = ", ";
        //    return string.Join(separator, addressFields.Where(field => !string.IsNullOrWhiteSpace(field)));
        //}
    }
}
