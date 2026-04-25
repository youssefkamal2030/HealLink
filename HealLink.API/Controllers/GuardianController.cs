using System;
using System.Threading.Tasks;
using healLink.Application.Commands.Guardian;
using healLink.Application.Queries.Guardian;
using HealLink.Contracts.Guardian.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GuardianController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuardianController(IMediator mediator) => _mediator = mediator;

        /// <summary>Assign a guardian to a patient. Creates the guardian if they don't exist yet.</summary>
        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignGuardianRequest request)
        {
            var result = await _mediator.Send(new AssignGuardianCommand(
                request.PatientId, request.GuardianUserId, request.RelationshipToPatient));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Remove the guardian from a patient.</summary>
        [HttpDelete("{patientId}/remove")]
        public async Task<IActionResult> Remove([FromRoute] Guid patientId)
        {
            var result = await _mediator.Send(new RemoveGuardianCommand(patientId));
            return result.IsSuccess ? Ok(new { message = "Guardian removed." }) : BadRequest(new { message = result.Error });
        }

        /// <summary>Get the guardian assigned to a patient.</summary>
        [HttpGet("{patientId}")]
        public async Task<IActionResult> Get([FromRoute] Guid patientId)
        {
            var result = await _mediator.Send(new GetGuardianQuery(patientId));
            return result.IsSuccess ? Ok(result.Value) : NotFound(new { message = result.Error });
        }
    }
}
