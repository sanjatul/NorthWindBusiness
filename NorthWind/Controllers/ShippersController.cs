using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class ShippersController : Controller
    {
        private readonly NorthwindContext _context;
        public ShippersController(NorthwindContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var shippers = await _context.Shippers.ToListAsync();
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Shipper model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _context.Shippers.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int shipperId)
        {
            var model = await _context.Shippers.FindAsync(shipperId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Shipper model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Shippers.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int shipperId)
        {
            var model = await _context.Shippers.FindAsync(shipperId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Shipper model)
        {

            _context.Shippers.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var shippers = await _context.Shippers.ToListAsync();
            return Json(new { data = shippers });
        }
        #endregion

    }
}
