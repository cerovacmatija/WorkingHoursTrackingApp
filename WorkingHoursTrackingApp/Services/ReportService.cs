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

        // Method to get working hours for a single employee in a custom time range
        public IEnumerable<string> GetWorkingHoursForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            var employee = _context.Employees.Find(employeeId);
            if (employee == null)
            {
                return new List<string> { "Employee not found" };
            }

            var workingHoursForEmployee = _context.WorkingHours
                .Where(wh => wh.EmployeeId == employeeId && wh.StartTime >= startDate && wh.EndTime <= endDate)
                .ToList();

            if (workingHoursForEmployee.Count == 0)
            {
                return new List<string> { $"No working hours found for {employee.FirstName} {employee.LastName} and the specified time range" };
            }

            // Calculate total working time
            var totalWorkingTime = workingHoursForEmployee
                .Select(wh => wh.EndTime.Value - wh.StartTime)
                .Aggregate(TimeSpan.Zero, (acc, timeSpan) => acc.Add(timeSpan));

            // Format total working time
            var formattedTotalTime = FormatTimeSpanString(totalWorkingTime);

            // Group working hours by date
            var workingHoursByDate = workingHoursForEmployee
                .GroupBy(wh => wh.StartTime.Date)
                .ToList();

            // Generate output
            var output = new List<string>
    {
        $"Total working hours for {employee.FirstName} {employee.LastName}: {formattedTotalTime}"
    };

            foreach (var group in workingHoursByDate)
            {
                var date = group.Key;
                var totalWorkingHoursOnDate = group
                    .Select(wh => wh.EndTime.Value - wh.StartTime)
                    .Aggregate(TimeSpan.Zero, (acc, timeSpan) => acc.Add(timeSpan));
                var formattedWorkingHoursOnDate = FormatTimeSpanString(totalWorkingHoursOnDate);

                output.Add($"{date.ToShortDateString()}: {formattedWorkingHoursOnDate}");
            }

            return output;
        }

        // Method to get working hours for all employees in a custom time range, ordered by the amount of working hours descending
        public IEnumerable<string> GetEmployeesByWorkingHoursDescending(DateTime startDate, DateTime endDate)
        {
            var employeesWithHours = _context.WorkingHours
                .Where(w => w.StartTime >= startDate && w.StartTime <= endDate && w.EndTime != null)
                .GroupBy(w => w.EmployeeId)
                .ToList() // Execute the query in memory
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    TotalHours = g.Sum(w => (w.EndTime.Value - w.StartTime).TotalHours),
                    WorkDetails = g.Select(w => new
                    {
                        Date = w.StartTime.Date,
                        WorkHours = (w.EndTime.Value - w.StartTime).TotalHours
                    })
                })
                .OrderByDescending(e => e.TotalHours)
                .ToList(); // Execute the projection in memory

            var result = new List<string>();
            foreach (var item in employeesWithHours)
            {
                var employee = _context.Employees.Find(item.EmployeeId);
                if (employee != null)
                {
                    var employeeName = $"{employee.FirstName} {employee.LastName}";
                    result.Add($"Total working hours for {employeeName}: {FormatTimeSpanString(TimeSpan.FromHours(item.TotalHours))}");

                    foreach (var workDetail in item.WorkDetails)
                    {
                        result.Add($"Date: {workDetail.Date.ToShortDateString()}, Work Hours: {FormatTimeSpanString(TimeSpan.FromHours(workDetail.WorkHours))}");
                    }

                    // Add a blank line between each employee's details
                    result.Add(string.Empty);
                }
            }
            return result;
        }

        private string FormatTimeSpanString(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
            {
                return "0 hours, 0 minutes, 0 seconds";
            }

            var parts = new List<string>();

            if (timeSpan.Hours > 0)
            {
                parts.Add($"{timeSpan.Hours} {(timeSpan.Hours == 1 ? "hour" : "hours")}");
            }

            if (timeSpan.Minutes > 0)
            {
                parts.Add($"{timeSpan.Minutes} {(timeSpan.Minutes == 1 ? "minute" : "minutes")}");
            }

            if (timeSpan.Seconds > 0)
            {
                parts.Add($"{timeSpan.Seconds} {(timeSpan.Seconds == 1 ? "second" : "seconds")}");
            }

            return string.Join(", ", parts);
        }

    }
}
