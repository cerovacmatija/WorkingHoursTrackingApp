using Microsoft.AspNetCore.Mvc;
using EmployeeTrackingApp.Services;
using System;

namespace EmployeeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("schedule")]
        public IActionResult CreateSchedule([FromBody] ScheduleRequest request)
        {
            // Validate request and create schedule
            _scheduleService.CreateSchedule(request.EmployeeId, request.Date, request.StartTime, request.EndTime);
            return Ok("Schedule created successfully");
        }

        [HttpPut("schedule/{scheduleId}")]
        public IActionResult UpdateSchedule(int scheduleId, [FromBody] ScheduleRequest request)
        {
            // Validate request and update schedule
            _scheduleService.UpdateSchedule(scheduleId, request.EmployeeId, request.Date, request.StartTime, request.EndTime);
            return Ok("Schedule updated successfully");
        }

        [HttpDelete("schedule/{scheduleId}")]
        public IActionResult DeleteSchedule(int scheduleId)
        {
            // Delete schedule
            _scheduleService.DeleteSchedule(scheduleId);
            return Ok("Schedule deleted successfully");
        }

        [HttpGet("all-schedules")]
        public IActionResult GetAllSchedulesWithEmployees()
        {
            var schedules = _scheduleService.GetAllSchedulesWithEmployees();
            return Ok(schedules);
        }

        [HttpGet("employees-working-on/{date}")]
        public IActionResult GetEmployeesWorkingOnDate(DateTime date)
        {
            var employees = _scheduleService.GetEmployeesWorkingOnDate(date);
            return Ok(employees);
        }

        [HttpGet("work-days-for-employee/{employeeId}")]
        public IActionResult GetWorkDaysForEmployee(int employeeId)
        {
            var workDays = _scheduleService.GetWorkDaysForEmployee(employeeId);
            return Ok(workDays);
        }
    }

    public class ScheduleRequest
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
