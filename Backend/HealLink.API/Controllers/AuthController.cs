using Microsoft.AspNetCore.Mvc;
using MediatR;
using HealLink.Contracts.Auth;
using healLink.Application.Commands;
using System.Threading.Tasks;
using healLink.Application.Commands.Auth;
using Microsoft.AspNetCore.Http;
using HealLink.Infrastructure.Services;
using healLink.Application.Interfaces;
using HealLink.Contracts.Email;
using ErrorOr;
using Microsoft.AspNetCore.Identity.Data;

namespace HealLink.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IMediator mediator, IPhotoService photoService, IEmailService emailService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPhotoService _photoService = photoService;
        private readonly IEmailService _emailService = emailService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Contracts.Auth.RegisterRequest request)
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
        public async Task<IActionResult> Login([FromBody] Contracts.Auth.LoginRequest request)
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
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data" });

            try
            {
                var isConfirmed = await _emailService.ConfirmEmailAsync(request);

                if (isConfirmed)
                    return Ok(new { message = "Email confirmed successfully" });
                else
                    return BadRequest(new { message = "Invalid email or OTP code" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }



        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] Contracts.Auth.ForgotPasswordRequest request)
        {
            await _emailService.SendResetOtpAsync(request.Email);
         
            return Ok(new { Message = "If an account with that email exists, a password reset link has been sent." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] Contracts.Auth.ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(request.Email, request.code, request.NewPassword);
            var result = await _mediator.Send(command);
            if (result.Message == "Password reset Successfully")
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] Contracts.Auth.ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            await _emailService.ResendConfirmationEmailAsync(request.Email);
           return Ok();

        }
    }
}
