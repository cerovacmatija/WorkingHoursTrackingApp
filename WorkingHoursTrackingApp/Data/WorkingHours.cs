using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTrackingApp.Data
{
    public class WorkingHours
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
