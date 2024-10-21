using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageEmployee.Controllers
{
    public class CommuneController : Controller
    {
        private readonly CommuneService _communeService;
        private readonly DistrictService _districtService;

        public CommuneController(CommuneService comuneService, DistrictService districtService)
        {
            _communeService = comuneService;
            _districtService = districtService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _communeService.GetCommunes());
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
            //gửi lại danh sách District nếu tạo Commune lỗi
            var listDistrict = await _districtService.GetDistricts();
            var selectedList = listDistrict.Select(d => new SelectListItem
            {
                Value = d.DistrictId.ToString(),
                Text = $"{d.DistrictName} - {d.Province.ProvinceName}"
            }).ToList();

            ViewBag.Communes = selectedList;

            if (commune.DistrictId == 0)
            {
                ViewBag.Error = "Please select the province that contains this district.";
                return View();  // Return the view with validation message
            }

            //kiểm tra tên Commune trong District
            if (_communeService.IsCommuneExistedInDistrict(commune.CommuneName, commune.DistrictId))
            {
                var district = listDistrict.FirstOrDefault(d => d.DistrictId == commune.DistrictId);
                string districtName = district != null ? district.DistrictName : "Unknown  district";
                TempData["Error"] = $"District '{commune.CommuneName}' has existed in {districtName}";
                return View();
            }

            _communeService.AddCommune(commune);
            TempData["Success"] = "Create commune successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var commune = _communeService.GetCommuneById(id.Value);
            if (commune == null)
            {
                return NotFound();
            }

            var listDistrict = _districtService.GetDistricts().Result;
            var selectedList = listDistrict.Select(d => new SelectListItem
            {
                Value = d.DistrictId.ToString(),
                Text = $"{d.DistrictName} - {d.Province.ProvinceName}"
            }).ToList();
            ViewBag.Communes = selectedList;

            return View(commune);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Commune commune)
        {
            var listDistrict = _districtService.GetDistricts().Result;
            var selectedList = listDistrict.Select(d => new SelectListItem
            {
                Value = d.DistrictId.ToString(),
                Text = $"{d.DistrictName} - {d.Province.ProvinceName}"
            }).ToList();
            ViewBag.Communes = selectedList;

            if (_communeService.IsCommuneExistedInDistrict(commune.CommuneName, commune.DistrictId))
            {
                var district = selectedList.FirstOrDefault(c => c.Value == commune.CommuneId.ToString());
                string districtName = district != null ? district.Text.ToString() : "Unknown  district";
                TempData["Error"] = $"Commune '{commune.CommuneName}' has existed in {districtName}";
                return View();
            }

            _communeService.UpdateCommune(commune);
            TempData["Success"] = "Update commune successfull";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int communeId)
        {
            var commune = _communeService.GetCommuneById(communeId);
            if (commune == null)
            {
                TempData["Error"] = "District not found";
                return NotFound();
            }
           
            _communeService.RemoveCommune(commune);
            TempData["Success"] = "Delete commune successfully";
            return Ok();
        }
    }
}
