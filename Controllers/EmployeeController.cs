using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
