using ErrorOr;
using HealLink.Contracts.Doctors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ApiController
    {
        private ISender _sender;

        public DoctorController(ISender sender) 
        {
            _sender=sender;
        }

        [HttpPut("UpdateDoctorData/{DoctorId}")]
        public async Task<IActionResult> AddDoctorData(DoctorDataRequest request)
        {
            var command = request.ToCommand();
            var resuelt =await _sender.Send(command);
            return resuelt.Match(
                success=> Ok("The Data Has Been Updated."),
                error=> Problem(error));

        }
        [HttpGet("GetDoctorHistory/{DoctorId}")]
        public async Task<IActionResult> GetSubscriptionHistory(Guid DoctorId)
        {
            var command = new GetSubscriptionHistoryQuery(DoctorId);
            var result = await _sender.Send(command);
            return result.Match(
                result=> Ok(result.ToList()),
                errors=>Problem(errors));
        }
    }



}
