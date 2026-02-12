using HealLink.Contracts.Chat.NewFolder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        public async Task<IActionResult> GetChatHistory(ChatHistoryRequest chatHistoryRequest)
        {
            var command = new GetChatHistoryCommand(chatHistoryRequest.UserId1, chatHistoryRequest.UserId2);
        }
}
