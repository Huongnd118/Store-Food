using Microsoft.AspNetCore.Mvc;
using Store_Food.Models;

namespace Store_Food.Components
{
    public class Navbar : ViewComponent
    {
        private readonly DataContext _context;

        public Navbar(DataContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Categories.ToList());
        }
    }
}
