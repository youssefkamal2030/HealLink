using ErrorOr;
using healLink.Application.Admins.Commands.ApproveDoctor;
using healLink.Application.Admins.Commands.ChangeTheRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ApiController
    {
        private ISender _sender;

        public AdminController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("ApproveDoctor/{DoctorId}")]
        public async Task<IActionResult> ApproveDoctor(Guid DoctorId)
        {
            var command =new ApproveDoctorCommand(DoctorId);
            var result = await _sender.Send(command);
            return result.Match(
                Success=>Ok(DoctorId),
            errors =>Problem(errors));
        }
        [HttpPut("ChangeRoleToDoctor/{DoctorId}")]
        public async Task<IActionResult> ChangeRoleToDoctor(Guid DoctorId)
        {
            var command = new ChangeRoleToDoctorCommand(DoctorId);
            var result = await _sender.Send(command);
            return result.Match(
                success=> Ok(success),
                errors => Problem(errors));
        }

    }
}
