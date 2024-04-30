using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeTrackingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrackingApp.Services
{
    public class ScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateSchedule(int employeeId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var schedule = new Schedule
            {
                EmployeeId = employeeId,
                Date = date.Date,
                StartTime = startTime,
                EndTime = endTime
            };

            _context.Schedules.Add(schedule);
            _context.SaveChanges();
        }

        public void UpdateSchedule(int scheduleId, int newEmployeeId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var schedule = _context.Schedules.Find(scheduleId);
            if (schedule == null)
            {
                throw new ArgumentException("Schedule not found");
            }

            schedule.EmployeeId = newEmployeeId;
            schedule.Date = date.Date;
            schedule.StartTime = startTime;
            schedule.EndTime = endTime;

            _context.SaveChanges();
        }

        public void DeleteSchedule(int scheduleId)
        {
            var schedule = _context.Schedules.Find(scheduleId);
            if (schedule == null)
            {
                throw new ArgumentException("Schedule not found");
            }

            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
        }

        public IEnumerable<Schedule> GetEmployeeSchedule(int employeeId, DateTime startDate, DateTime endDate)
        {
            return _context.Schedules
                .Where(s => s.EmployeeId == employeeId && s.Date >= startDate.Date && s.Date <= endDate.Date)
                .ToList();
        }

        public IEnumerable<ScheduleWithEmployee> GetAllSchedulesWithEmployees()
        {
            return _context.Schedules
                .Include(s => s.Employee) // Include the Employee navigation property
                .Select(s => new ScheduleWithEmployee
                {
                    ScheduleId = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee != null ? $"{s.Employee.FirstName} {s.Employee.LastName}" : "Unknown", // Null check before accessing properties
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                })
                .ToList();
        }

        public IEnumerable<Employee> GetEmployeesWorkingOnDate(DateTime date)
        {
            return _context.Schedules
                .Where(s => s.Date.Date == date.Date)
                .Select(s => s.Employee)
                .Distinct()
                .ToList();
        }

        public IEnumerable<DateTime> GetWorkDaysForEmployee(int employeeId)
        {
            return _context.Schedules
                .Where(s => s.EmployeeId == employeeId)
                .Select(s => s.Date.Date)
                .Distinct()
                .ToList();
        }
    }

    public class ScheduleWithEmployee
    {
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
