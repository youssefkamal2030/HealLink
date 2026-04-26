using System;
using System.Threading.Tasks;
using healLink.Application.Commands.Chat;
using healLink.Application.Queries.Chat;
using HealLink.Contracts.Chat.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    // TODO: [REFACTOR-AUTH] Remove manual JWT claim extraction after centralized-authorization-infrastructure is implemented
    // PROBLEM: Controller manually extracts user ID from JWT claims in EditMessage and DeleteMessage endpoints
    // FIX: Remove JWT claim extraction logic from controller
    // APPROACH: UserContextProvider will automatically extract user context from JWT in AuthorizationBehavior
    // REASON: Centralize user context extraction, reduce boilerplate in controllers
    // MIGRATION STEPS:
    //   1. Remove JWT claim extraction code from EditMessage (lines 57-63)
    //   2. Remove JWT claim extraction code from DeleteMessage (lines 79-85)
    //   3. Remove RequestingUserId parameter from EditMessageCommand and DeleteMessageCommand
    //   4. Controller will only pass MessageId and NewContent to commands
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator) => _mediator = mediator;

        /// <summary>Send a message between a connected doctor and patient.</summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var result = await _mediator.Send(new SendMessageCommand(request.SenderId, request.ReceiverId, request.Content));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Get chat history between two users.</summary>
        [HttpGet("history")]
        public async Task<IActionResult> GetChatHistory([FromQuery] Guid userId1, [FromQuery] Guid userId2)
        {
            var result = await _mediator.Send(new GetChatHistoryQuery(userId1, userId2));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Mark a message as delivered. Enforces Sent → Delivered transition.</summary>
        [HttpPut("{messageId}/delivered")]
        public async Task<IActionResult> MarkAsDelivered([FromRoute] Guid messageId)
        {
            var result = await _mediator.Send(new MarkAsDeliveredCommand(messageId));
            return result.IsSuccess ? Ok() : BadRequest(new { message = result.Error });
        }

        /// <summary>Mark a message as read. Enforces Delivered → Read transition.</summary>
        [HttpPut("{messageId}/read")]
        public async Task<IActionResult> MarkAsRead([FromRoute] Guid messageId)
        {
            var result = await _mediator.Send(new MarkAsReadCommand(messageId));
            return result.IsSuccess ? Ok() : BadRequest(new { message = result.Error });
        }

        /// <summary>Edit a message content. Only the sender can edit their own message.</summary>
        [HttpPut("{messageId}")]
        public async Task<IActionResult> EditMessage([FromRoute] Guid messageId, [FromBody] EditMessageRequest request)
        {
            // Extract user ID from JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                           ?? User.FindFirst("sub");
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var authenticatedUserId))
            {
                return Unauthorized(new { success = false, message = "Unable to identify user from token" });
            }

            var command = new EditMessageCommand(messageId, authenticatedUserId, request.NewContent);
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(new { success = true, message = "Message edited successfully" })
                : BadRequest(new { success = false, message = result.Error });
        }

        /// <summary>Soft delete a message. Only the sender can delete their own message.</summary>
        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid messageId)
        {
            // Extract user ID from JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                           ?? User.FindFirst("sub");
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var authenticatedUserId))
            {
                return Unauthorized(new { success = false, message = "Unable to identify user from token" });
            }

            var command = new DeleteMessageCommand(messageId, authenticatedUserId);
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(new { success = true, message = "Message deleted successfully" })
                : BadRequest(new { success = false, message = result.Error });
        }

        /// <summary>Search messages between two users by content.</summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchMessages([FromQuery] Guid userId1, [FromQuery] Guid userId2, [FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest(new { message = "Search term is required" });

            var query = new SearchMessagesQuery(userId1, userId2, searchTerm);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(new { message = result.Error });
        }
    }
}
