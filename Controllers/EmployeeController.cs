using ManageEmployee.Models;
using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Text;
using System.Text.RegularExpressions;
using X.PagedList;
using X.PagedList.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManageEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly int _pageSize = 10;
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index(string searchText, int? provinceId, int? districtId, int? communeId, int pageNumber = 1)
        {
            int pageSize = _pageSize; //số lượng bản ghi mỗi trang
            var employeesQuery = _employeeService.GetEmployees().AsQueryable();

            //lấy danh sách Province
            ViewBag.Provinces = _employeeService.GetProvinces().Select(p => new SelectListItem
            {
                Value = p.ProvinceId.ToString(),
                Text = p.ProvinceName.Trim(),
                Selected = provinceId.HasValue && provinceId.Value == p.ProvinceId
            });
            //lấy danh sách District
            ViewBag.Districts = _employeeService.GetDistricts().Select(d => new SelectListItem
            {
                Value= d.DistrictId.ToString(),
                Text = d.DistrictName.Trim(),
                Selected = districtId.HasValue && districtId.Value == d.DistrictId
            });
            //lấy danh sách Commune
            ViewBag.Communes = _employeeService.GetCommunesBy().Select(c => new SelectListItem
            {
                Value = c.CommuneId.ToString(),
                Text = c.CommuneName.Trim(),
                Selected = communeId.HasValue && communeId.Value == c.CommuneId
            });

            //lưu lại các giá trị đã chọn
            ViewBag.SelectedProvinceId = provinceId;
            ViewBag.SelectedDistrictId = districtId;
            ViewBag.SelectedCommuneId = communeId;

            //search
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                employeesQuery = _employeeService.SearchEmployees(searchText).AsQueryable();
            }

            //filter
            if (provinceId.HasValue)
            {
                employeesQuery = employeesQuery.Where(e => e.ProvinceId == provinceId.Value);
            }
            if (districtId.HasValue)
            {
                employeesQuery = employeesQuery.Where(e => e.DistrictId == districtId.Value);
            }
            if (communeId.HasValue)
            {
                employeesQuery = employeesQuery.Where(e => e.CommuneId == communeId.Value);
            }

            var pagedEmployees = employeesQuery.ToPagedList(pageNumber, pageSize);
            return View(pagedEmployees);
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

            // lấy danh sách Province
            var provinces = _employeeService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");

            // lấy danh sách District thông qua ProvinceId
            var districts = _employeeService.GetDistrictsByProvinceId(employee.ProvinceId);
            ViewBag.Districts = new SelectList(districts, "DistrictId", "DistrictName");

            //lấy danh sách Commune thông qua DistrictId
            var communes = _employeeService.GetCommunesByDistrictId(employee.DistrictId);
            ViewBag.Communes = new SelectList(communes, "CommuneId", "CommuneName");

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

            // lấy danh sách Province
            var provinces = _employeeService.GetProvinces();
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");

            // lấy danh sách District thông qua ProvinceId
            var districts = _employeeService.GetDistrictsByProvinceId(obj.ProvinceId);
            ViewBag.Districts = new SelectList(districts, "DistrictId", "DistrictName");

            //lấy danh sách Commune thông qua DistrictId
            var communes = _employeeService.GetCommunesByDistrictId(obj.DistrictId);
            ViewBag.Communes = new SelectList(communes, "CommuneId", "CommuneName");
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
            var districts = _employeeService.GetDistrictsByProvinceId(provinceId);
            return Ok(districts); 
        }

        [HttpGet]
        public IActionResult GetCommunesByDistrict(int districtId)
        {
            var communes = _employeeService.GetCommunesByDistrictId(districtId);
            return Ok(communes);
        }

        [HttpGet]
        public IActionResult Details(int? id)
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

            return View(employee);
        }

        [HttpPost]
        public IActionResult ExportSelectedEmployees([FromBody] List<int> employeeIds)
        {
            if (employeeIds == null || !employeeIds.Any())
            {
                return BadRequest("No Employee IDs provided.");
            }

            var employees = _employeeService.GetEmployees()
            .Where(e => employeeIds.Contains(e.Id))
            .Select(e => new
            {
                e.Id,
                e.FullName,
                e.DOB,
                e.Ethnicity,
                e.Job,
                e.CitizenId,
                e.PhoneNumber,
                Province = e.Province?.ProvinceName ?? "N/A",
                District = e.District?.DistrictName ?? "N/A",
                Commune = e.Commune?.CommuneName ?? "N/A",
                AddressDetail = e.AddressDetails ?? "N/A",
                TotalDiploma = e.Diplomas?.Count()??0,
                Diplomas = e.Diplomas.Select(d => new
                {
                    d.Name,
                    d.IssuedDate,
                    d.ExpiryDate,
                    IssuedByProvince = d.IssuedByProvince.ProvinceName
                })
            }).ToList(); 

            if (!employees.Any())
            {
                return NotFound("No employees found with the provided IDs.");
            }

            // Set up Excel file generation using EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Full Name";
                worksheet.Cells[1, 3].Value = "Date of Birth";
                worksheet.Cells[1, 4].Value = "Ethnicity";
                worksheet.Cells[1, 5].Value = "Job";
                worksheet.Cells[1, 6].Value = "Citizen ID";
                worksheet.Cells[1, 7].Value = "Phone Number";
                worksheet.Cells[1, 8].Value = "Province";
                worksheet.Cells[1, 9].Value = "District";
                worksheet.Cells[1, 10].Value = "Commune";
                worksheet.Cells[1, 11].Value = "Address Details";
                worksheet.Cells[1, 12].Value = "Diplomas";
                worksheet.Cells[1, 13].Value = "Issued Date";
                worksheet.Cells[1, 14].Value = "Expiry Date";
                worksheet.Cells[1, 15].Value = "Issued By Province";

                // dòng thứ row
                int row = 2;
                foreach (var emp in employees)
                {
                    worksheet.Cells[row, 1].Value = emp.Id;
                    worksheet.Cells[row, 2].Value = emp.FullName;
                    worksheet.Cells[row, 3].Value = emp.DOB.ToString("dd-MM-yyyy");
                    worksheet.Cells[row, 4].Value = emp.Ethnicity;
                    worksheet.Cells[row, 5].Value = emp.Job;
                    worksheet.Cells[row, 6].Value = emp.CitizenId;
                    worksheet.Cells[row, 7].Value = emp.PhoneNumber;
                    worksheet.Cells[row, 8].Value = emp.Province;
                    worksheet.Cells[row, 9].Value = emp.District;
                    worksheet.Cells[row, 10].Value = emp.Commune;
                    worksheet.Cells[row, 11].Value = emp.AddressDetail;

                    //diploma
                    int totalDiplomas = emp.TotalDiploma;
                    var diplomasList = emp.Diplomas.ToList(); 

                    for (int i = 0; i < totalDiplomas; i++)
                    {
                        int baseColumn = 12 + i * 4;
                        if (i < diplomasList.Count)
                        {
                            var diploma = diplomasList[i];
                            worksheet.Cells[row, baseColumn].Value = diploma.Name;
                            worksheet.Cells[row, baseColumn + 1].Value = diploma.IssuedDate.ToString("dd-MM-yyyy");
                            worksheet.Cells[row, baseColumn + 2].Value = diploma.ExpiryDate?.ToString("dd-MM-yyyy") ?? "N/A";
                            worksheet.Cells[row, baseColumn + 3].Value = diploma.IssuedByProvince;
                        }
                    }
                    row++;

                }

                var excelFileContent = package.GetAsByteArray();
                return File(excelFileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SelectedEmployees.xlsx");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportEmployeesFromExcel(IFormFile file)
        {
            var errors = new List<string>();
            int countEmployee = 0;
            int rowCount;
            using (var steam = new MemoryStream())
            {
                await file.CopyToAsync(steam);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(steam))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employee = new Employee
                        {
                            FullName = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                            DOB = DateTime.TryParse(worksheet.Cells[row, 2].Value?.ToString(), out var dob) ? dob : DateTime.MinValue,
                            Ethnicity = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Job = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                            CitizenId = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                            PhoneNumber = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                        };
                        var validationErrors = ValidateEmployeeData(employee, row);
                        if (validationErrors.Any())
                        {
                            errors.AddRange(validationErrors);
                        }
                        else
                        {
                            await _employeeService.AddEmployeeAsync(employee);
                            countEmployee++;
                        }
                    }
                }
            }
            TempData["Success"] = $"successfully added {countEmployee}/{rowCount - 1} Employee(s)";
            // Chuyển list errors thành string với mỗi lỗi trên một dòng mới
            if (errors.Any())
            {
                TempData["Error"] = string.Join("<br/>", errors);
            }
            return RedirectToAction("Index");
        }

        public List<string> ValidateEmployeeData(Employee employee, int row)
        {
            var errors = new List<string>();
            if (employee.DOB == DateTime.MinValue)
            {
                errors.Add($"Row {row}: Invalid date of birth.");
            }
            if (string.IsNullOrEmpty(employee.Ethnicity))
            {
                errors.Add($"Row {row}: Ethnicity cannot be left blank.");
            }
            if (string.IsNullOrEmpty(employee.Job))
            {
                errors.Add($"Row {row}: Job cannot be left blank.");
            }
            if (employee.CitizenId != null && !Regex.IsMatch(employee.CitizenId, @"^\d{12}$"))
            {
                errors.Add($"Row {row}: CitizenId must consist of exactly 12 digits.");
            }
            if (employee.PhoneNumber != null && !Regex.IsMatch(employee.PhoneNumber, @"^\d{10}$"))
            {
                errors.Add($"Row {row}: PhoneNumber must consist of exactly 10 digits.");
            }
            return errors;
        }

    }
}
