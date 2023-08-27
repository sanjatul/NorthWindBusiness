using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;
using NorthWind.Models.ViewModels;

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
            //var categories = await _context.Categories.ToListAsync();
            return View();
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

        public async Task<IActionResult> Edit(int categoryId)
        {
            var model=await _context.Categories.FindAsync(categoryId);
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

        public async Task<IActionResult> Delete(int categoryId)
        {
            var model = await _context.Categories.FindAsync(categoryId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category model)
        {
            
            _context.Categories.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return Json(new { data = categories });
        }
        #endregion
    }
}
