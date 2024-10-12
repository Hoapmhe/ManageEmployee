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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Province province)
        {
            if (_provinceService.IsProvinceExisted(province.ProvinceName))
            {
                ModelState.AddModelError("ProvinceError", $"Province '{province.ProvinceName}' has existed");
            }
            if (ModelState.IsValid)
            {
                _provinceService.AddProvince(province);
                TempData["Success"] = "Create province successfully";
                return RedirectToAction("Index");
            }
            return View();
        } 

    }
}
