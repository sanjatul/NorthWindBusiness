using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NorthWind.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;
        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
     .Include(p => p.Category)
     .Include(p => p.Supplier)
     .Select(p => new
     {
         p.ProductId,
         p.ProductName,
         p.Unit,
         p.Price,
         p.CategoryId,
         p.SupplierId,
         CategoryName = p.Category.CategoryName,
         SupplierName = p.Supplier.SupplierName,
         // Other relevant fields
     })
     .ToListAsync();
            var jsonResult = Json(new { data = products });
            return jsonResult;

        }

        #endregion


    }
}
