using healLink.Application.Commands.Notifications;
using healLink.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all notifications for a doctor
        /// </summary>
        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> GetAllNotificationsForDoctor([FromRoute] Guid doctorId)
        {
            var query = new GetAllDoctorNotificatonsQuery(doctorId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        /// <summary>
        /// Get all notifications for a patient
        /// </summary>
        [HttpGet("Patient/{patientId}")]
        public async Task<IActionResult> GetAllNotificationsForPatient([FromRoute] Guid patientId)
        {
            var query = new GetAllPatientNotificationsQuery(patientId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        /// <summary>
        /// Mark a notification as read
        /// </summary>
        [HttpPut("{notificationId}/MarkAsRead")]
        public async Task<IActionResult> MarkAsRead([FromRoute] Guid notificationId)
        {
            var command = new MarkNotificationAsReadCommand(notificationId);
            var result = await _mediator.Send(command);
            return result.IsSuccess 
                ? Ok(new { message = "Notification marked as read successfully" }) 
                : BadRequest(new { message = result.Error });
        }
    }
}
