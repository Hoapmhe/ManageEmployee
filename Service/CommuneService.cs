using ManageEmployee.Data;
using ManageEmployee.Models;

namespace ManageEmployee.Service
{
    public class CommuneService
    {
        private readonly AppDbContext _context;
        public CommuneService(AppDbContext context)
        {
            _context = context;
        }
        public List<Commune> GetCommunes()
        {
            return _context.Communes.ToList();
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
    }
}
