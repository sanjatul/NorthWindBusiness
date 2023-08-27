
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;
using NorthWind.Models.ViewModels;
using System.Diagnostics;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        //public IActionResult GetAll()
        //{
        //    var query = from od in _context.Orderdetails
        //                join o in _context.Orders on od.OrderId equals o.OrderId
        //                join c in _context.Customers on o.CustomerId equals c.CustomerId
        //                join p in _context.Products on od.ProductId equals p.ProductId
        //                join cat in _context.Categories on p.CategoryId equals cat.CategoryId
        //                orderby (od.Quantity * p.Price) descending
        //                select new
        //                {
        //                    od.OrderId,
        //                    c.CustomerName,
        //                    p.ProductName,
        //                    o.OrderDate,
        //                    od.Quantity,
        //                    p.Price,
        //                    Amount = od.Quantity * p.Price
        //                };

        //    var result = query
        //        .Select(item => new TopBuyers
        //        {
        //            OrderId = (int)item.OrderId,
        //            CustomerName = item.CustomerName,
        //            ProductName = item.ProductName,
        //            OrderDate = item.OrderDate,
        //            Quantity = item.Quantity,
        //            Price = item.Price,
        //            Amount = (decimal)item.Amount
        //        })
        //        .ToList(); 
        //    return Json(new { data = result });
        //}



        public IActionResult GetAll(int draw, int start, int length, string searchValue)
        {
            
            // Apply filtering, ordering, and pagination here
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


            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(item =>
                    item.CustomerName.Contains(searchValue) ||
                    item.ProductName.Contains(searchValue));
            }

            // Calculate total records before pagination
            int totalRecords = query.Count();

            // Apply pagination
            query = query.Skip(start).Take(length);

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
                .ToList();

            return Json(new
            {
                draw = draw, // Increase the draw counter for each request
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords, // Since we're not implementing filtering here
                data = result
            });
        }

        #endregion

    }
}