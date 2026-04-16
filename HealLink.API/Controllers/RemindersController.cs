using System;
using System.Threading.Tasks;
using healLink.Application.Commands.Prescriptions;
using healLink.Application.Queries.Prescriptions;
using HealLink.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemindersController(IMediator mediator) => _mediator = mediator;

        /// <summary>Get medication reminders for a patient, optionally filtered by date and/or status.</summary>
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetReminders(
            [FromRoute] Guid patientId,
            [FromQuery] DateTime? date = null,
            [FromQuery] MedicationReminderStatus? status = null)
        {
            var result = await _mediator.Send(new GetMedicationRemindersQuery(patientId, date, status));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Mark a medication reminder as taken.</summary>
        [HttpPut("{reminderId}/taken")]
        public async Task<IActionResult> MarkAsTaken(
            [FromRoute] Guid reminderId,
            [FromQuery] Guid patientId,
            [FromQuery] Guid actingUserId)
        {
            var result = await _mediator.Send(new MarkReminderAsTakenCommand(reminderId, patientId, actingUserId));
            return result.IsSuccess ? Ok(new { message = "Reminder marked as taken." }) : BadRequest(new { message = result.Error });
        }
    }
}
