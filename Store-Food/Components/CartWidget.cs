using Microsoft.AspNetCore.Mvc;
using Store_Food.Infrastructure;
using Store_Food.Models;

namespace Store_Food.Components
{
    public class CartWidget : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
