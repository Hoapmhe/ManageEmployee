﻿using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var listEmployee = _employeeService.GetEmployees();
            return View(listEmployee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee obj)
        {
            if (_employeeService.IsCitizenIdExited(obj.CitizenId))
            {
                ModelState.AddModelError("EmployeeError", $"Citizen id '{obj.CitizenId}' has existed");
            }
            if (ModelState.IsValid)
            {
                _employeeService.AddEmployee(obj);
                TempData["Success"] = "Create employee successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
