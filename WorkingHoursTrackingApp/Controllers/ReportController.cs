using Microsoft.AspNetCore.Mvc;
using EmployeeTrackingApp.Services;
using System;
using System.Collections.Generic;

namespace EmployeeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;
        private readonly ScheduleService _scheduleService;

        public ReportController(ReportService reportService, ScheduleService scheduleService)
        {
            _reportService = reportService;
            _scheduleService = scheduleService;
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
