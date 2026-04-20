using System;
using System.Threading.Tasks;
using healLink.Application.Commands.MedicalHistory;
using healLink.Application.Queries.MedicalHistory;
using HealLink.Contracts.MedicalHistory.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicalHistoryController(IMediator mediator) => _mediator = mediator;

        /// <summary>Get a patient's medical history.</summary>
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> Get([FromRoute] Guid patientId)
        {
            var result = await _mediator.Send(new GetMedicalHistoryQuery(patientId));
            return result.IsSuccess ? Ok(result.Value) : NotFound(new { message = result.Error });
        }

        /// <summary>Create or replace a patient's medical history.</summary>
        [HttpPut("patient/{patientId}")]
        public async Task<IActionResult> Update([FromRoute] Guid patientId, [FromBody] UpdateMedicalHistoryRequest request)
        {
            var command = new UpdateMedicalHistoryCommand(
                patientId,
                request.ChronicConditions,
                request.Allergies,
                request.CurrentMedications,
                request.PreviousSurgeries,
                request.FamilyHistory,
                request.Notes,
                request.FileLink);

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }
    }
}
