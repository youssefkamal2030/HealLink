using healLink.Application.Commands.Connections;
using HealLink.Contracts.Connections.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ConnectionsController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator Mediator = _mediator;

        [HttpPost("RequestConnection")]
        public async Task<IActionResult> RequestConnection(CreateConnectionRequest request)
        {
            var Command = new CreateConnectionRequestCommand(request.DoctorId, request.PatientId);
            var result = await _mediator.Send(Command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

     
    }
}
