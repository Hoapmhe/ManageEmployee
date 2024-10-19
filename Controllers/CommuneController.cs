using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageEmployee.Controllers
{
    public class CommuneController : Controller
    {
        private readonly CommuneService _comuneService;
        private readonly DistrictService _districtService;

        public CommuneController(CommuneService comuneService, DistrictService districtService)
        {
            _comuneService = comuneService;
            _districtService = districtService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _comuneService.GetCommunes());
        }

        public async Task<IActionResult> Create()
        {
            var listDistrict = await _districtService.GetDistricts();

            var selectedList = listDistrict.Select(d => new SelectListItem
            {
                Value = d.DistrictId.ToString(),
                Text = $"{d.DistrictName} - {d.Province.ProvinceName}"
            }).ToList();

            ViewBag.Communes = selectedList;

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Commune commune)
        {
            _comuneService.AddCommune(commune);
            TempData["Success"] = "Create commune successfully";
            return RedirectToAction("Index");
        }
    }
}
