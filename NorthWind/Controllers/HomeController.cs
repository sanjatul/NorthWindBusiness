using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;
using NorthWind.Models.ViewModels;
using System.Diagnostics;

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


        public IActionResult Index(int page = 1, int pageSize = 10)
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

            int totalItems = 100;

            var result = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
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
                .Take(100).ToList(); ;

            var viewModel = new TopBuyersViewModel
            {
                TopBuyersList = result,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}