using ManageEmployee.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
