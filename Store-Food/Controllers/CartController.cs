using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store_Food.Infrastructure;
using Store_Food.Models;
using System.Text.Json;
using NuGet.Protocol.Core.Types;
//using CodeMegaVNPay.Services;

namespace Store_Food.Controllers
{
    public class CartController : Controller
    {
        //private readonly IVnPayService _vnPayService;
        public Cart? Cart { get; set; } = new Cart();
        private readonly DataContext? _context;

        public CartController(DataContext? context/*, IVnPayService vnPayService*/)
        {
            _context = context;
            //_vnPayService = vnPayService;
        }
        
        public IActionResult Index()
        {
            return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
        }

        
        public IActionResult AddToCart(int foodId)
        {
            Food? food = _context.Foods.FirstOrDefault(p => p.FoodId == foodId);
            if (food != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(food, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("cart", Cart);
        }

        public IActionResult RemoveFromCart(int foodId)
        {
            Food? food = _context.Foods.FirstOrDefault(p => p.FoodId == foodId);
            if (food != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(food);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("cart", Cart);
        }

        public IActionResult UpdateCart(int foodId)
        {
            Food? food = _context.Foods.FirstOrDefault(p => p.FoodId == foodId);
            if (food != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(food, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("cart", Cart);
        }

        //public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        //{
        //    var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

        //    return Json(url);
        //}

        //public IActionResult PaymentCallback()
        //{
        //    var response = _vnPayService.PaymentExecute(Request.Query);

        //    return Json(response);
        //}
    }
}
