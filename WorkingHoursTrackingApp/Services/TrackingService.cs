using System;
using EmployeeTrackingApp.Data;

namespace EmployeeTrackingApp.Services
{
    public class TrackingService
    {
        private readonly ApplicationDbContext _context;

        public TrackingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void StartTimeTracking(int employeeId)
        {
            var trackingEntry = new WorkingHours
            {
                EmployeeId = employeeId,
                StartTime = DateTime.Now
            };
            _context.WorkingHours.Add(trackingEntry);
            _context.SaveChanges();
        }

        public void EndTimeTracking(int employeeId)
        {
            var trackingEntry = _context.WorkingHours.FirstOrDefault(t => t.EmployeeId == employeeId && t.EndTime == null);
            if (trackingEntry != null)
            {
                trackingEntry.EndTime = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
