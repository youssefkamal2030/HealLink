using healLink.Application.Commands.Doctor;
using healLink.Application.Commands.Doctors;
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

        public DoctorsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Get all patients connected to a doctor.</summary>
        [HttpGet("{doctorId}/ConnectedPatients")]
        public async Task<IActionResult> GetConnectedPatients([FromRoute] Guid doctorId)
        {
            var result = await _mediator.Send(new GetConnectedPatientsQuery(doctorId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        /// <summary>Approve a doctor account. Admin only.</summary>
        /// refactor: this endpoint should belong to a dedicated Admin Controller with proper admin requests and responses
        [HttpPost("{doctorId}/approve")]
        public async Task<IActionResult> ApproveDoctor([FromRoute] Guid doctorId)
        {
            var result = await _mediator.Send(new ApproveDoctorCommand(doctorId));
            return result.IsSuccess ? Ok(new { message = "Doctor approved successfully." }) : BadRequest(new { message = result.Error });
        }
        [HttpPost("{doctorId}/reject")]
        public async Task<IActionResult> RejectDoctor([FromBody] Guid doctorId, string reason , Guid adminId )
        {
            var result = await _mediator.Send(new RejectDoctorCommand(doctorId,reason,adminId));
            return result.IsSuccess ? Ok(new { message = "Doctor rejected successfully." }) : BadRequest(new { message = result.Error });
        }
    }
}
