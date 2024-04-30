using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTrackingApp.Data
{
    public class Schedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Navigation property for Employee
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}
