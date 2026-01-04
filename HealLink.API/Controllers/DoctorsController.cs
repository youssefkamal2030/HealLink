using healLink.Application.Commands.Connections;
using healLink.Application.Queries;
using HealLink.Contracts.Connections.Requests;
using HealLink.Contracts.Doctor.Requests;
using HealLink.Contracts.Doctor.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{doctorId}/ConnectedPatients")]
        public async Task<IActionResult> GetConnectedPatients([FromRoute] Guid doctorId)
        {
            var query = new GetConnectedPatientsQuery(doctorId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Accept")]
        public async Task<IActionResult> AcceptConnection(AcceptConnectionRequest request)
        {
            var command = new AcceptConnectionCommand(request.ConnectionId, request.DoctorId);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { message = "Connection accepted successfully" })
                : BadRequest(new { message = result.Error });
        }

        [HttpPost("Reject")]
        public async Task<IActionResult> RejectConnection(RejectConnectionRequest request)
        {
            var command = new RejectConnectionCommand(request.ConnectionId, request.DoctorId);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { message = "Connection rejected successfully" })
                : BadRequest(new { message = result.Error });
        }
    }
}