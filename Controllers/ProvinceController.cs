using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly ProvinceService _provinceService;
        public ProvinceController(ProvinceService provinceService)
        {
            _provinceService = provinceService;
        }
        public async Task<IActionResult> Index()
        {
            var provinces = await _provinceService.GetProvinces();
            return View(provinces);
        }



    }
}
