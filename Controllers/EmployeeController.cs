using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var listEmployee = _employeeService.GetEmployees();
            return View(listEmployee);
        }

        public IActionResult Create()
        {
            var provinces = _employeeService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee obj)
        {
            if (_employeeService.IsCitizenIdExited(obj.CitizenId))
            {
                ModelState.AddModelError("EmployeeError", $"Citizen id '{obj.CitizenId}' has existed");
            }
            if (ModelState.IsValid)
            {
                _employeeService.AddEmployee(obj);
                TempData["Success"] = "Create employee successfully";
                return RedirectToAction("Index");
            }
            //lấy lại danh sách Province trong trường hợp lỗi
            var provinces = _employeeService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.EmployeeName = employee.FullName;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee obj)
        {
            var empCitizenId = _employeeService.GetEmployeeById(obj.Id).CitizenId;
            if (!obj.CitizenId.Equals(empCitizenId) && _employeeService.IsCitizenIdExited(obj.CitizenId))
            {
                ModelState.AddModelError("EmployeeError", $"Citizen id '{obj.CitizenId}' has existed");
            }
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(obj);
                TempData["Success"] = "Update employee successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found";
                return NotFound();
            }
            else
            {
                _employeeService.DeleteEmployee(employee);
                TempData["Success"] = "Delete employee successfully";
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult GetDistrictsByProvince(int provinceId)
        {
            var districts = _employeeService.GetDistricts(provinceId);
            return Ok(districts); 
        }

        [HttpGet]
        public IActionResult GetCommunesByDistrict(int districtId)
        {
            var communes = _employeeService.GetCommunes(districtId);
            return Ok(communes);
        }
    }
}
