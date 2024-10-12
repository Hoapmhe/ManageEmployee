using ManageEmployee.Data;
using ManageEmployee.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployee.Service
{
    public class ProvinceService
    {
        private readonly AppDbContext _context;
        public ProvinceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Province>> GetProvinces()
        {
            return await _context.Provinces.ToListAsync();
        }

        public bool IsProvinceExisted(string provinceName)
        {
            return _context.Provinces
                   .Any(p => p.ProvinceName.ToLower() == provinceName.ToLower());
        }
        public void AddProvince(Province province)
        {
            _context.Provinces.Add(province);
            _context.SaveChanges();
        }
        public void UpdateProvince(Province province)
        {
            _context.Provinces.Update(province);
            _context.SaveChanges();
        }
        public void RemoveProvince(Province province)
        {
            _context.Provinces.Remove(province);
            _context.SaveChanges();
        }

        public Province GetProvinceById(int id)
        {
            return _context.Provinces.AsNoTracking().FirstOrDefault(e => e.ProvinceId == id);
        }
    }
}
