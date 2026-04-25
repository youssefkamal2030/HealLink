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
    }
}
