using Microsoft.AspNetCore.Mvc;
using Store_Food.Models;

namespace Store_Food.Components
{
    public class JustArrived : ViewComponent
    {
        private readonly DataContext _context;

        public JustArrived(DataContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Foods.Where(p => p.IsArriver == true).ToList());
        }
    }
}
