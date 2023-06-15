using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Food.Models;
using Store_Food.Models.ViewModels;

namespace Store_Food.Controllers
{
    public class FoodsController : Controller
    {
        private readonly DataContext _context;
        public int PageSize = 9;

        public FoodsController(DataContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index(int foodPage = 1)
        {

            return View(
                new FoodListViewModel
                {
                    Foods = _context.Foods.Skip((foodPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerpage = PageSize,
                        CurrentPage = foodPage,
                        TotalItems = _context.Foods.Count()
                    }
                }
                );
        }


        [HttpPost]
        public async Task<IActionResult> Search(string keywords, int foodPage = 1)
        {

            return View("Index",
                new FoodListViewModel
                {
                    Foods = _context.Foods
                    .Where(p => p.FoodName.Contains(keywords))
                    .Skip((foodPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerpage = PageSize,
                        CurrentPage = foodPage,
                        TotalItems = _context.Foods.Count()
                    }
                }
                );
        }

        public async Task<IActionResult> FoodsByCart(int categoryId)
        {
            var applicationDbContext = _context.Foods.Where(p => p.CategoryId == categoryId).Include(f => f.Category).Include(f => f.Color).Include(f => f.Size);
            return View("Index", await applicationDbContext.ToListAsync());
        }
        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Color)
                .Include(f => f.Size)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["ColorId"] = new SelectList(_context.Set<Color>(), "ColorId", "ColorId");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,FoodDescription,CategoryId,FoodPrice,FoodDiscount,FoodPhoto,SizeId,ColorId,IsTrandy,IsArriver")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", food.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Set<Color>(), "ColorId", "ColorId", food.ColorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", food.SizeId);
            return View(food);
        }

        // GET: Foods/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", food.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Set<Color>(), "ColorId", "ColorId", food.ColorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", food.SizeId);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,FoodDescription,CategoryId,FoodPrice,FoodDiscount,FoodPhoto,SizeId,ColorId,IsTrandy,IsArriver")] Food food)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.FoodId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", food.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Set<Color>(), "ColorId", "ColorId", food.ColorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", food.SizeId);
            return View(food);
        }

        // GET: Foods/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Color)
                .Include(f => f.Size)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Foods == null)
            {
                return Problem("Entity set 'DataContext.Foods'  is null.");
            }
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
          return (_context.Foods?.Any(e => e.FoodId == id)).GetValueOrDefault();
        }
    }
}
