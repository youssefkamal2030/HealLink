using System;
using System.Threading.Tasks;

using HealLink.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestResultsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Upload a test result for a patient. Can be done by the patient or their guardian.</summary>
        //[HttpPost]
        //public async Task<IActionResult> Upload([FromForm] Guid patientId, [FromForm] Guid actingUserId,
        //    [FromForm] string testName, [FromForm] string description, [FromForm] DateTime testDate,
        //    IFormFile file, [FromForm] FileType fileType)
        //{
        //    //var command = new UploadTestResultCommand(patientId, actingUserId, testName, description, testDate, file, fileType);
        //    var result = await _mediator.Send(command);
        //    return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        //}

        ///// <summary>Get all test results for a patient.</summary>
        //[HttpGet("patient/{patientId}")]
        //public async Task<IActionResult> GetByPatient([FromRoute] Guid patientId)
        //{
        //    var result = await _mediator.Send(new GetTestResultsQuery(patientId));
        //    return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        //}
    }
}
