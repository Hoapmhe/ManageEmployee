using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class CommuneController : Controller
    {
        private readonly CommuneService _comuneService;

        public CommuneController(CommuneService comuneService)
        {
            _comuneService = comuneService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _comuneService.GetCommunes());
        }
    }
}
