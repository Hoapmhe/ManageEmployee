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
    }
}
