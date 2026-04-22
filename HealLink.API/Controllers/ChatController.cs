using healLink.Application.Queries;
using healLink.Application.Queries.Chat;
using HealLink.Contracts.Chat.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    // TODO: [MISSING-FEATURE] SendMessage endpoint is absent — ChatHub handles real-time sending but there is no REST
    //   fallback. Add POST /api/Chat/Send that dispatches a SendMessageCommand (create it) which calls
    //   ChatRepository.AddChatMessageAsync() and UnitOfWork.SaveChangesAsync(). The hub can call the same command.
    // TODO: [MISSING-FEATURE] No endpoint to mark messages as Delivered or Read. Add:
    //   PUT /api/Chat/{messageId}/delivered → MarkAsDeliveredCommand
    //   PUT /api/Chat/{messageId}/read      → MarkAsReadCommand
    //   Both go through ChatMessage.MarkAsDelivered() / MarkAsRead() which already have transition guards.
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get chat history between two users
        /// </summary>
        [HttpGet("History")]
        public async Task<IActionResult> GetChatHistory([FromQuery] Guid userId1, [FromQuery] Guid userId2)
        {
            var query = new GetChatHistoryQuery(userId1, userId2);
            var result = await _mediator.Send(query);
            
            return result.IsSuccess 
                ? Ok(result.Value) 
                : BadRequest(new { message = result.Error });
        }
    }
}
