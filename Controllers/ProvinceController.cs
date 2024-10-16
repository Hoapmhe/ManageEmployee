using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly ProvinceService _provinceService;
        private readonly DistrictService _districtService;
        public ProvinceController(ProvinceService provinceService, DistrictService districtService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var province = _provinceService.GetProvinceById(id.Value);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Province obj)
        {
            if (_provinceService.IsProvinceExisted(obj.ProvinceName))
            {
                ModelState.AddModelError("ProvinceError", $"Province '{obj.ProvinceName}' has existed");
            }
            if (ModelState.IsValid)
            {
                _provinceService.UpdateProvince(obj);
                TempData["Success"] = "Update province successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int provinceId)
        {
            var province = _provinceService.GetProvinceById(provinceId);

            if (province == null)
            {
                TempData["Error"] = "Province not found";
                return NotFound();
            }

            // Fetch the districts that belong to the province
            var districts = _districtService.GetListDistrictsByProvinceId(provinceId);

            // Delete all the districts associated with this province
            foreach (var district in districts)
            {
                _districtService.RemoveDistrict(district);
            }

            _provinceService.RemoveProvince(province);

            TempData["Success"] = "Deleted province and its districts successfully";
            return Ok();
        }

    }
}
