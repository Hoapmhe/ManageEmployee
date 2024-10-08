using ManageEmployee.Data;
using ManageEmployee.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listEmployee = _context.Employees.ToList();
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
            bool citizenIdExists = _context.Employees.Any(e => e.CitizenId == obj.CitizenId);
            if (citizenIdExists)
            {
                ModelState.AddModelError("EmployeeError", $"Citizen id '{obj.CitizenId}' has existed");
            }
            if (ModelState.IsValid)
            {
                _context.Employees.Add(obj);
                _context.SaveChanges();
                TempData["Sucess"] = "Create employee sucessesfull";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
