using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class CustomersController : Controller
    {
        private readonly NorthwindContext _context;
        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
           // var customers = await _context.Customers.ToListAsync();
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _context.Customers.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int customerId)
        {
            var model = await _context.Customers.FindAsync(customerId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Customers.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int customerId)
        {
            var model = await _context.Customers.FindAsync(customerId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Customer model)
        {

            _context.Customers.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            return Json(new { data = customers });
        }
        #endregion
    }
}
