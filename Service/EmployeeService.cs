﻿using ManageEmployee.Data;
using ManageEmployee.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            return _context.Employees
                .Include(e => e.Commune)
                .Include(e => e.District)
                .Include(e => e.Province)
                .Include(e => e.Diplomas).ThenInclude(d => d.IssuedByProvince)
                .ToList();
        }

        //Using AsNoTracking() for retrievals or detaching the existing tracked entity
        //should resolve the issue of tracking multiple instances of the same entity with the same key value.
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees
                .Include(e => e.Commune)
                .Include(e => e.District)
                .Include(e => e.Province)
                .Include(e => e.Diplomas).ThenInclude(d => d.IssuedByProvince)
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
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

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Province> GetProvinces()
        {
            return _context.Provinces.ToList();
        }

        public IEnumerable<District> GetDistricts()
        {
            return _context.Districts.ToList();
        }

        public IEnumerable<Commune> GetCommunesBy()
        {
            return _context.Communes.ToList();
        }

        public IEnumerable<Employee> SearchEmployees(string employeeName)
        {
            return _context.Employees.Where(e => e.FullName.Contains(employeeName)).ToList();
        }

        public IEnumerable<District> GetDistrictsByProvinceId(int? provinceId)
        {
            return _context.Districts.Where(d => d.ProvinceId == provinceId).ToList();
        }

        public IEnumerable<Commune> GetCommunesByDistrictId(int? districtId)
        {
            return _context.Communes.Where(c => c.DistrictId == districtId).ToList();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
    }
}
