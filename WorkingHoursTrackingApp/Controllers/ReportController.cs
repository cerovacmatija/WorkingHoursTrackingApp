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

        [HttpGet("working-hours/{employeeId}")]
        public IActionResult GetWorkingHoursForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            var workingHours = _reportService.GetWorkingHoursForEmployee(employeeId, startDate, endDate);
            return Ok(workingHours);
        }

        [HttpGet("all-employees")]
        public IActionResult GetEmployeesByWorkingHoursDescending(DateTime startDate, DateTime endDate)
        {
            var employees = _reportService.GetEmployeesByWorkingHoursDescending(startDate, endDate).ToList();
            return Ok(employees);
        }
    }
}
