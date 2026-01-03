using healLink.Application.Commands.Connections;
using healLink.Application.Queries;
using HealLink.Contracts.Connections.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConnectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new connection request from patient to doctor
        /// </summary>
        [HttpPost("Request")]
        public async Task<IActionResult> RequestConnection(CreateConnectionRequest request)
        {
            var command = new CreateConnectionRequestCommand(request.DoctorId, request.PatientId);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        /// <summary>
        /// Doctor accepts a connection request
        /// </summary>
        [HttpPost("Accept")]
        public async Task<IActionResult> AcceptConnection(AcceptConnectionRequest request)
        {
            var command = new AcceptConnectionCommand(request.ConnectionId, request.DoctorId);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { message = "Connection accepted successfully" })
                : BadRequest(new { message = result.Error });
        }

        /// <summary>
        /// Doctor rejects a connection request
        /// </summary>
        [HttpPost("Reject")]
        public async Task<IActionResult> RejectConnection(RejectConnectionRequest request)
        {
            var command = new RejectConnectionCommand(request.ConnectionId, request.DoctorId);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { message = "Connection rejected successfully" })
                : BadRequest(new { message = result.Error });
        }

        /// <summary>
        /// Get pending connection requests for a doctor
        /// </summary>
        [HttpGet("Doctor/{doctorId}/Pending")]
        public async Task<IActionResult> GetPendingConnections([FromRoute] Guid doctorId)
        {
            var query = new GetPendingConnectionsQuery(doctorId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        /// <summary>
        /// Get all connections for a doctor
        /// </summary>
        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorConnections([FromRoute] Guid doctorId)
        {
            var query = new GetDoctorConnectionsQuery(doctorId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        /// <summary>
        /// Get all connections for a patient
        /// </summary>
        [HttpGet("Patient/{patientId}")]
        public async Task<IActionResult> GetPatientConnections([FromRoute] Guid patientId)
        {
            var query = new GetPatientConnectionsQuery(patientId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
