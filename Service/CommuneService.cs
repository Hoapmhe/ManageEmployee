using ManageEmployee.Data;
using ManageEmployee.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployee.Service
{
    public class CommuneService
    {
        private readonly AppDbContext _context;
        public CommuneService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Commune>> GetCommunes()
        {
            return await _context.Communes.Include(c => c.District)
                                          .ThenInclude(d => d.Province) //lấy Province thông qua District
                                          .ToListAsync();
        }
        public bool IsCommuneExisted(string communeName)
        {
            return _context.Communes.Any(d => d.CommuneName.Equals(communeName, StringComparison.OrdinalIgnoreCase));
        }
        public void AddCommune(Commune commune)
        {
            _context.Communes.Add(commune);
            _context.SaveChanges();
        }
        public void UpdateCommune(Commune commune)
        {
            _context.Communes.Update(commune);
            _context.SaveChanges();
        }
        public void RemoveCommune(Commune commune)
        {
            _context.Communes.Remove(commune);
            _context.SaveChanges();
        }

        public IEnumerable<Commune> GetListCommunesByDistrictId(int communeId)
        {
            return _context.Communes.Where(d => d.CommuneId == communeId).ToList();
        }
    }
}
