using ManageEmployee.Data;
using ManageEmployee.Models;
using System.Collections.Generic;
using System.Linq;

namespace ManageEmployee.Service
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public bool IsCitizenIdExited(string citizenId)
        {
            return _context.Employees.Any(e => e.CitizenId == citizenId);
        }
    }
}
