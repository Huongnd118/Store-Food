using Microsoft.AspNetCore.Mvc;
using Store_Food.Models;

namespace Store_Food.Components
{
    public class Imagebar : ViewComponent
    {
        private readonly DataContext _context;

        public Imagebar(DataContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View("Index", _context.Categories.ToList());
        }
    }
}
