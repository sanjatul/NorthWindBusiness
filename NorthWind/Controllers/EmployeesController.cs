﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly NorthwindContext _context;
        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var Employees = await _context.Employees.ToListAsync();
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int employeeId)
        {
            var model = await _context.Employees.FindAsync(employeeId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Employees.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int employeeId)
        {
            var model = await _context.Employees.FindAsync(employeeId);
            return View(model);
        }
        public async Task<IActionResult> Delete(int employeeId)
        {
            var model = await _context.Employees.FindAsync(employeeId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Employee model)
        {

            _context.Employees.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();
            return Json(new { data = employees });
        }
        #endregion
    }

}
