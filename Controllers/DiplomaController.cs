using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageEmployee.Controllers
{
    public class DiplomaController : Controller
    {
        private readonly DiplomaService _diplomaService;
        private readonly EmployeeService _employeeService;
        public DiplomaController(DiplomaService diplomaService, EmployeeService employeeService)
        {
            _diplomaService = diplomaService;
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int? employeeId)
        {
            if (employeeId == null || employeeId == 0)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var provinces = _employeeService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");

            ViewBag.EmployeeId = employeeId.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Diploma diploma)
        {
            //gửi lại danh sách Province nếu gặp lỗi
            var provinces = _employeeService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            //gửi lại thông tin EmployeeId
            ViewBag.EmployeeId = diploma.EmployeeId;

            var province = _employeeService.GetProvinces().FirstOrDefault(p => p.ProvinceId == diploma.IssuedByProvinceId);
            var employee = _employeeService.GetEmployeeById(diploma.EmployeeId);
            //kiểm tra trùng lặp Diploma
            if (_diplomaService.IsDiplomaInProvince(diploma.Name, 
                        diploma.IssuedByProvinceId, diploma.EmployeeId))
            {
                TempData["Warning"] = $"This Diploma was issued by the {province.ProvinceName} to {employee.FullName}";
                return View(diploma);
            }
            //Check the maximum number of valid diplomas
            if (!employee.CanAddDiploma())
            {
                TempData["Warning"] = $"{employee.FullName} has 3 valid Diplomas.";
                return View(diploma);
            }

            if (ModelState.IsValid)
            {
                _diplomaService.AddDiploma(diploma);
                TempData["Success"] = "Add diploma successfully";
                return RedirectToAction("Details", "Employee", new { id = diploma.EmployeeId });
            }
            return View(diploma);
        }
    }
}
