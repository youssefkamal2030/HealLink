using MediatR;
using Microsoft.AspNetCore.Mvc;
using healLink.Application.Queries;
using healLink.Application.Commands.Profile;
using HealLink.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using HealLink.Contracts.Profile.Requests;

namespace HealLink.Api.Controllers
{
    [Authorize]
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
                new Address(request.City, request.Country),
                request.Specialization,
                request.CurrentWorkplace,
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
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> DeleteDoctorProfile(Guid doctorId)
        {
            // Extract user ID from JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                           ?? User.FindFirst("sub");
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var authenticatedUserId))
            {
                return Unauthorized(new { success = false, message = "Unable to identify user from token" });
            }

            var command = new DeleteDoctorProfileCommand(doctorId, authenticatedUserId);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return result.Message.Contains("unauthorized") 
                ? Forbid() 
                : NotFound(result);
        }

        /// <summary>Update a patient's profile (username and email).</summary>
        [HttpPut("patient/{patientId}")]
        public async Task<IActionResult> UpdatePatientProfile(Guid patientId, [FromBody] UpdatePatientProfileRequest request)
        {
            // Extract user ID from JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                           ?? User.FindFirst("sub");
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var authenticatedUserId))
            {
                return Unauthorized(new { success = false, message = "Unable to identify user from token" });
            }

            var command = new UpdatePatientProfileCommand(
                patientId,
                authenticatedUserId,
                request.Username,
                request.Email
            );

            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(new { success = true, message = "Patient profile updated successfully" })
                : BadRequest(new { success = false, message = result.Error });
        }

        /// <summary>Delete a patient profile. Only the patient or an admin can delete.</summary>
        [HttpDelete("patient/{patientId}")]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> DeletePatientProfile(Guid patientId)
        {
            // Extract user ID from JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                           ?? User.FindFirst("sub");
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var authenticatedUserId))
            {
                return Unauthorized(new { success = false, message = "Unable to identify user from token" });
            }

            var command = new DeletePatientProfileCommand(patientId, authenticatedUserId);
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(new { success = true, message = "Patient profile deleted successfully" })
                : result.Error.Contains("authorized")
                    ? Forbid()
                    : BadRequest(new { success = false, message = result.Error });
        }
    }
}
