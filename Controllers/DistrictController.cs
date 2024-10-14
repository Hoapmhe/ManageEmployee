using ManageEmployee.Service;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
    public class DistrictController : Controller
    {
        private readonly DistrictService _districtService;
        public DistrictController(DistrictService districtService)
        {
            _districtService = districtService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _districtService.GetDistricts();
            return View(list);
        }
    }
}
