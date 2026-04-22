using healLink.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    // TODO: [MISSING-FEATURE] Doctor approval endpoint is absent — Doctor.Approve() exists and raises DoctorApprovedEvent
    //   but there is no way to trigger it. Add POST /api/Doctors/{doctorId}/approve (admin only).
    //   Create ApproveDoctorCommand + handler that loads the doctor, calls doctor.Approve(doctorId), saves.
    // TODO: [MISSING-FEATURE] No endpoint to get a single doctor's profile by doctorId. Currently only connected
    //   patients are exposed. Add GET /api/Doctors/{doctorId} for profile retrieval.
    // TODO: [AUTH] No role-based authorization on any endpoint — any authenticated user can call ConnectedPatients
    //   for any doctorId. Add [Authorize(Roles = "Doctor")] or a policy check that the caller owns the doctorId.
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
