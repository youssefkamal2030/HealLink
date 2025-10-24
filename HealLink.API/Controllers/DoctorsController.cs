using healLink.Application.Queries;
using HealLink.Contracts.Doctor.Requests;
using HealLink.Contracts.Doctor.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
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
    }
}