using Microsoft.AspNetCore.Mvc;
using MediatR;
using healLink.Application.Commands;
using System.Threading.Tasks;
using healLink.Application.Commands.Auth;
using Microsoft.AspNetCore.Http;
using HealLink.Infrastructure.Services;
using healLink.Application.Interfaces;
using HealLink.Contracts.Auth.Requests;
using HealLink.Contracts.Email;
using healLink.Application.Commands.Auth;

namespace HealLink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator, IPhotoService photoService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPhotoService _photoService = photoService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!Enum.TryParse<HealLink.Domain.Enums.UserRole>(request.Role, true, out var userRole))
                return BadRequest("Invalid role");

            var command = new RegisterCommand(request.username, request.Password, request.Email, userRole, request.Specilization, request.PracticeLisenceNumber, request.SyndicateId);
            var result = await _mediator.Send(command);
            return result.Message == "User registered successfully" ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var command = new LoginCommand(request.Email, request.Password);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            var command = new ConfirmEmailCommand(request.Email, request.Code);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { message = "Email confirmed successfully." })
                : BadRequest(new { message = result.Error });
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest request)
        {
            var command = new ResendOtpCommand(request.Email);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { message = "If your email is registered and unconfirmed, a new OTP has been sent." })
                : BadRequest(new { message = result.Error });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            await _mediator.Send(new ForgotPasswordCommand(request.Email));
            return Ok(new { Message = "If an account with that email exists, a password reset link has been sent." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(request.Email, request.Token, request.NewPassword);
            var result = await _mediator.Send(command);
            return result.Message == "Password reset Successfully" ? Ok(result) : BadRequest(result);
        }
    }
}
