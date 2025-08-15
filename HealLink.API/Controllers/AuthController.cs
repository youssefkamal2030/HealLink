using Microsoft.AspNetCore.Mvc;
using MediatR;
using HealLink.Contracts.Auth;
using healLink.Application.Commands;
using System.Threading.Tasks;
using healLink.Application.Commands.Auth;
using Microsoft.AspNetCore.Http;
using HealLink.Infrastructure.Services;
using healLink.Application.Interfaces;

namespace HealLink.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IMediator mediator, IPhotoService photoService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPhotoService _photoService = photoService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!Enum.TryParse<HealLink.Domain.Enums.UserRole>(request.Role, true, out var userRole))
            {
                return BadRequest("Invalid role");
            }
            var command = new RegisterCommand(request.username, request.Password, request.Email, userRole, request.Idfront , request.Idback);
            var frontUrl = await _photoService.SavePhotoAsync(request.Idfront, "uploads");
            var backUrl = await _photoService.SavePhotoAsync(request.Idback, "uploads");
            var result = await _mediator.Send(command);
            if (result.Message == "User registered successfully")
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var command = new LoginCommand(request.Email, request.Password);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _mediator.Send(new ForgotPasswordCommand(request.Email));
         
            return Ok(new { Message = "If an account with that email exists, a password reset link has been sent." });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(request.Email, request.Token, request.NewPassword);
            var result = await _mediator.Send(command);
            if (result.Message == "Password reset Successfully")
                return Ok(result);
            return BadRequest(result);
        }
    }
}
