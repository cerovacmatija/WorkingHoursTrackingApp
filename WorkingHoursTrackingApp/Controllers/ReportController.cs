using Microsoft.AspNetCore.Mvc;
using EmployeeTrackingApp.Services;
using System;
using System.Linq;

namespace EmployeeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        // HTTP endpoint to get working hours for a single employee in a custom time range
        [HttpGet("working-hours/{employeeId}")]
        public IActionResult GetWorkingHoursForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            var workingHours = _reportService.GetWorkingHoursForEmployee(employeeId, startDate, endDate);
            return Ok(workingHours);
        }

        // HTTP endpoint to get working hours for all employees in a custom time range, ordered by the amount of working hours descending
        [HttpGet("all-employees")]
        public IActionResult GetEmployeesByWorkingHoursDescending(DateTime startDate, DateTime endDate)
        {
            var employees = _reportService.GetEmployeesByWorkingHoursDescending(startDate, endDate).ToList();
            return Ok(employees);
        }
    }
}
