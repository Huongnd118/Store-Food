using Microsoft.AspNetCore.Mvc;
using Store_Food.Models;

namespace Store_Food.Components
{
    public class Trandy : ViewComponent
    {
        private readonly DataContext _context;

        public Trandy(DataContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Foods.Where(p => p.IsTrandy == true).ToList());
        }
    }
}
