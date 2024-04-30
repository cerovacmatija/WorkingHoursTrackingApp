using System;
using System.Linq;
using EmployeeTrackingApp.Data;

namespace EmployeeTrackingApp.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateEmployee(Employee employee)
        {
            employee.CreatedAt = DateTime.Now;
            employee.LastUpdatedAt = DateTime.Now;
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public Employee? GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<Employee> GetEmployees()
        {
            return _context.Employees.AsQueryable();
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.LastUpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
