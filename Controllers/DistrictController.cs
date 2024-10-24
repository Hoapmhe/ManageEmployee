﻿using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageEmployee.Controllers
{
    public class DistrictController : Controller
    {
        private readonly DistrictService _districtService;
        private readonly ProvinceService _provinceService;
        private readonly CommuneService _communeService;
        public DistrictController(DistrictService districtService, 
                                  ProvinceService provinceService,
                                  CommuneService communeService)
        {
            _districtService = districtService;
            _provinceService = provinceService;
            _communeService = communeService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _districtService.GetDistricts();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var provinceList = await _provinceService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinceList, "ProvinceId", "ProvinceName"); //Gửi danh sách Province tới view
            
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(District district)
        {
            var provinceList = await _provinceService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinceList, "ProvinceId", "ProvinceName"); // Reassign the provinces for the datalist

            if (district.ProvinceId == 0)
            {
                ViewBag.Error = "Please select the province that contains this district.";
                return View();  // Return the view with validation message
            }

            if (_districtService.IsDistrictExistedInProvince(district.DistrictName, district.ProvinceId))
            {
                var province = provinceList.FirstOrDefault(p => p.ProvinceId == district.ProvinceId);
                string provinceName = province != null ? province.ProvinceName : "Unknown  province";
                TempData["Error"] = $"District '{district.DistrictName}' has existed in {provinceName}";
                return View();
            }

            _districtService.AddDistrict(district);
            TempData["Success"] = "Create distric successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var district = _districtService.GetDistrictById(id.Value);
            if (district == null)
            {
                return NotFound();
            }

            // Get the list of provinces and pre-select the current province
            var provinceList = _provinceService.GetProvinces().Result;
            ViewBag.Provinces = new SelectList(provinceList, "ProvinceId", "ProvinceName", district.ProvinceId);

            return View(district);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(District district)
        {
            //gửi lại danh sách chọn Province, nếu update lỗi
            var provinceList =  await _provinceService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinceList, "ProvinceId", "ProvinceName");

            var selectedDistrict = _districtService.GetDistrictById(district.DistrictId);
            //bỏ qua tên kiểm tra trùng lặp nếu sử dung lại tên District cũ
            if (!district.DistrictName.Equals(selectedDistrict.DistrictName, StringComparison.OrdinalIgnoreCase) 
                    && _districtService.IsDistrictExistedInProvince(district.DistrictName, district.ProvinceId))
            {
                var province = provinceList.FirstOrDefault(p => p.ProvinceId == district.ProvinceId);
                string provinceName = province != null ? province.ProvinceName : "Unknown  province";
                TempData["Error"] = $"District '{district.DistrictName}' has existed in {provinceName}";
                return View();
            }
            _districtService.UpdateDistrict(district);
            TempData["Success"] = "Update district successfull";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int districtId)
        {
            var district = _districtService.GetDistrictById(districtId);
            if (district == null)
            {
                TempData["Error"] = "District not found";
                return NotFound();
            }

            var communes = _communeService.GetListCommunesByDistrictId(districtId);
            foreach (var commune in communes)
            {
                _communeService.RemoveCommune(commune);
            }

            _districtService.RemoveDistrict(district);
            TempData["Success"] = "Delete district successfully";
            return Ok();
        }
    }
}
