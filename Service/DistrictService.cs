using ManageEmployee.Data;
using ManageEmployee.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployee.Service
{
    public class DistrictService
    {
        private readonly AppDbContext _context;
        public DistrictService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<District>> GetDistricts()
        {
            return await _context.Districts.Include(d => d.Province).ToListAsync();
        }
        public bool IsDistrictExistedInProvince(string districtName, int provinceId)
        {
            return _context.Districts
                    .Any(d => d.ProvinceId == provinceId && d.DistrictName.ToLower().Trim() == districtName.ToLower().Trim());
        }
        public void AddDistrict(District district)
        {
            _context.Districts.Add(district);
            _context.SaveChanges();
        }
        public void UpdateDistrict(District district)
        {
            _context.Districts.Update(district);
            _context.SaveChanges();
        }
        public void RemoveDistrict(District district)
        {
            _context.Districts.Remove(district);
            _context.SaveChanges();
        }

        public District GetDistrictById(int id)
        {
            return _context.Districts.AsNoTracking().FirstOrDefault(d => d.DistrictId == id);
        }

        public IEnumerable<District> GetListDistrictsByProvinceId(int provinceId)
        {
            return _context.Districts.Where(d => d.ProvinceId == provinceId).ToList();
        }
    }
}
