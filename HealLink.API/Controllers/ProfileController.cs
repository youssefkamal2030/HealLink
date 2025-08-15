using MediatR;
using Microsoft.AspNetCore.Mvc;
using healLink.Application.Queries;
using healLink.Application.Commands.Profile;
using HealLink.Contracts.Profile;
using HealLink.Domain.ValueObjects;

namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var query = new GetProfileQuery(userId);
            var result = await _mediator.Send(query);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? roleFilter = null)
        {
            var query = new GetAllProfilesQuery(page, pageSize, searchTerm, roleFilter);
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctorProfile(Guid doctorId, [FromBody] UpdateDoctorProfileRequest request)
        {
            var command = new UpdateDoctorProfileCommand(
                doctorId,
                new PersonalInfo(request.FullName, request.Gender, request.Nationality),
                new Address(request.Street, request.City, request.State, request.Country),
                request.Specialization,
                request.CurrentWorkplace,
                request.Phone,
                request.IsAvailableForChat
            );

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> DeleteDoctorProfile(Guid doctorId)
        {
            var command = new DeleteDoctorProfileCommand(doctorId);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

      
    }
}
