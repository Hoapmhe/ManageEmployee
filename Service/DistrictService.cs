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
            return _context.Provinces.Any(p => p.ProvinceId == provinceId) 
                && _context.Districts.Any(d => d.DistrictName.ToLower() == districtName.ToLower());
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
    }
}
