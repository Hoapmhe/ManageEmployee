using ManageEmployee.Data;
using ManageEmployee.Models;

namespace ManageEmployee.Service
{
    public class DiplomaService
    {
        private readonly AppDbContext _context;
        public DiplomaService(AppDbContext context)
        {
            _context = context;
        }

        public void AddDiploma(Diploma diploma)
        {
            _context.Diplomas.Add(diploma); 
            _context.SaveChanges();
        }

        public bool IsDiplomaInProvince(string diplomaName, int provinceId, int employeeId)
        {
            return _context.Diplomas.Any(d => d.EmployeeId == employeeId
                                       && d.IssuedByProvinceId == provinceId
                                       && d.Name.Trim() == diplomaName.Trim());
        }
    }
}
