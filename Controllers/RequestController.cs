using ErrorOr;
using healLink.Application.Requests.Commands.AcceptRequest;
using healLink.Application.Requests.Commands.CancelPatientRequest;
using healLink.Application.Requests.Commands.MakeNewSubscriptionRequest;
using healLink.Application.Requests.Commands.RejectRequest;
using healLink.Application.Requests.Queries.GetRequestsByDoctorId;

using HealLink.Contracts.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ApiController
    {
        private readonly ISender _sender;
        public RequestController(ISender sender)
        {
            _sender = sender;
        }
        #region The following methods are used to handle Doctorside requests operations.
        [HttpGet("GetRequests/{Id}")]
        public async Task<IActionResult> GetRequestsByDoctorId(Guid Id)
        {
            var command = new GetRequestsByDoctorIdQuery(Id);
            var result = await _sender.Send(command);

            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );

        }

        [HttpPut("RejectRequest/{requestId}")]
        public async Task<IActionResult> RejectRequest(Guid requestId)
        {
            var command = new RejectRequestCommand(requestId);
            var result = await _sender.Send(command);

            return result.Match(
                Success => Ok(),
                errors => Problem(errors));


        }
        [HttpPut("AcceptRequest/{requestId}")]
        public async Task<IActionResult> AcceptRequestCommand(Guid requestId)
        {
            var command = new AcceptRequestCommand(requestId);
            var result = await _sender.Send(command);
            return result.Match(
                Success => Ok(),
                error => Problem(error));


        }



        #endregion  





        #region The following methods are used to handle patient request operations.

        [HttpPost("MakeNewSubscriptionRequest")]
        public async Task<IActionResult> MakeNewSubscriptionRequest([FromBody] MakeRequestRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return result.Match(
                success => Ok("Request made successfully."),
                error => Problem(error)
            );
        }


        [HttpGet("GetPatientRequests/{userId}")]
        public async Task<IActionResult> GetPatientRequestsAsync(GetPatientRequest request)
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }


        [HttpGet("CancelRequest/{id}")]
        public async Task<IActionResult> CancelRequest(Guid id)
        {
            var command = new CancelRequestCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        #endregion

    }
}
