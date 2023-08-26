using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _context;
        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result=await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model=await _context.Categories.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Categories.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Categories.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category model)
        {
            
            _context.Categories.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
