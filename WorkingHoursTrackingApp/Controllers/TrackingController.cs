using Microsoft.AspNetCore.Mvc;
using EmployeeTrackingApp.Services;

namespace EmployeeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly TrackingService _trackingService;

        public TrackingController(TrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpPost("{employeeId}/start")]
        public IActionResult StartTimeTracking(int employeeId)
        {
            _trackingService.StartTimeTracking(employeeId);
            return Ok();
        }

        [HttpPost("{employeeId}/end")]
        public IActionResult EndTimeTracking(int employeeId)
        {
            _trackingService.EndTimeTracking(employeeId);
            return Ok();
        }
    }
}
