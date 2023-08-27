using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;
using NorthWind.Models.ViewModels;
using System.Diagnostics;
using System.Drawing.Printing;

namespace NorthWind.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _context;
        public HomeController(ILogger<HomeController> logger, NorthwindContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var query = from od in _context.Orderdetails
                        join o in _context.Orders on od.OrderId equals o.OrderId
                        join c in _context.Customers on o.CustomerId equals c.CustomerId
                        join p in _context.Products on od.ProductId equals p.ProductId
                        join cat in _context.Categories on p.CategoryId equals cat.CategoryId
                        orderby (od.Quantity * p.Price) descending
                        select new
                        {
                            od.OrderId,
                            c.CustomerName,
                            p.ProductName,
                            o.OrderDate,
                            od.Quantity,
                            p.Price,
                            Amount = od.Quantity * p.Price
                        };

            var result = query
                .Select(item => new TopBuyers
                {
                    OrderId = (int)item.OrderId,
                    CustomerName = item.CustomerName,
                    ProductName = item.ProductName,
                    OrderDate = item.OrderDate,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Amount = (decimal)item.Amount
                })
                .ToList(); ;
            return Json(new { data = result });
        }
        #endregion


    }
}