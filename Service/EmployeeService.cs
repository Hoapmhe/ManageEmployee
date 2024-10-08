﻿using ManageEmployee.Data;
using ManageEmployee.Models;
using Microsoft.EntityFrameworkCore;
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

        //Using AsNoTracking() for retrievals or detaching the existing tracked entity
        //should resolve the issue of tracking multiple instances of the same entity with the same key value.
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);
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