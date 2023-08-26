using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly NorthwindContext _context;
        public SuppliersController(NorthwindContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return View(suppliers);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Supplier model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _context.Suppliers.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Suppliers.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Supplier model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Suppliers.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Suppliers.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Supplier model)
        {

            _context.Suppliers.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
