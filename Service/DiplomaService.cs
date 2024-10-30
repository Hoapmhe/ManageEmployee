using ManageEmployee.Data;

namespace ManageEmployee.Service
{
    public class DiplomaService
    {
        private readonly AppDbContext _context;
        public DiplomaService(AppDbContext context)
        {
            _context = context;
        }
    }
}
