using healLink.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
