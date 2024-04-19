using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeTrackingApp.Data;

namespace EmployeeTrackingApp.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WorkingHours> GetWorkingHoursForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            return _context.WorkingHours.Where(w => w.EmployeeId == employeeId && w.StartTime >= startDate && w.StartTime <= endDate);
        }

        public IEnumerable<Employee> GetEmployeesByWorkingHoursDescending(DateTime startDate, DateTime endDate)
        {
            var employeesWithHours = _context.WorkingHours
                .Where(w => w.StartTime >= startDate && w.StartTime <= endDate && w.EndTime != null)
                .GroupBy(w => w.EmployeeId)
                .ToList() // Execute the query in memory
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    TotalHours = g.Sum(w => (w.EndTime.Value - w.StartTime).TotalHours)
                })
                .OrderByDescending(e => e.TotalHours)
                .ToList(); // Execute the projection in memory

            var employees = new List<Employee>();
            foreach (var item in employeesWithHours)
            {
                var employee = _context.Employees.Find(item.EmployeeId);
                if (employee != null)
                    employees.Add(employee);
            }
            return employees;
        }
    }
}
