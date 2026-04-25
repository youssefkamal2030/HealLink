using healLink.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Search for doctors with optional filters.
        /// </summary>
        /// <param name="searchTerm">Search in name, email, specialization, or workplace</param>
        /// <param name="specialization">Filter by specialization</param>
        /// <param name="city">Filter by city</param>
        /// <param name="country">Filter by country</param>
        /// <param name="isAvailableForChat">Filter by chat availability</param>
        /// <param name="isApprovedOnly">Show only approved doctors (default: true)</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20, max: 100)</param>
        [HttpGet("doctors")]
        public async Task<IActionResult> SearchDoctors(
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? specialization = null,
            [FromQuery] string? city = null,
            [FromQuery] string? country = null,
            [FromQuery] bool? isAvailableForChat = null,
            [FromQuery] bool? isApprovedOnly = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new SearchDoctorsQuery(
                searchTerm,
                specialization,
                city,
                country,
                isAvailableForChat,
                isApprovedOnly,
                page,
                pageSize);

            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(new { message = result.Error });
        }

        /// <summary>
        /// Search for patients with optional filters.
        /// </summary>
        /// <param name="searchTerm">Search in email or username</param>
        /// <param name="city">Filter by city</param>
        /// <param name="country">Filter by country</param>
        /// <param name="hasGuardian">Filter by guardian presence</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20, max: 100)</param>
        [HttpGet("patients")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> SearchPatients(
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? city = null,
            [FromQuery] string? country = null,
            [FromQuery] bool? hasGuardian = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new SearchPatientsQuery(
                searchTerm,
                city,
                country,
                hasGuardian,
                page,
                pageSize);

            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(new { message = result.Error });
        }
    }
}
