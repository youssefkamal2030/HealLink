using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Chat;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using HealLink.Contracts.Chat.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Chat
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result<ChatMessageDto>>
    {
        private readonly IChatService _chatService;

        public SendMessageCommandHandler(IChatService chatService)
            => _chatService = chatService;

        public async Task<Result<ChatMessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var validation = await _chatService.ValidateConnection(request.SenderId, request.ReceiverId);
            if (!validation.IsSuccess)
                return Result<ChatMessageDto>.Failure(validation.Error);

            var result = await _chatService.SendMessageAsync(request.SenderId, request.ReceiverId, request.Content);
            if (!result.IsSuccess)
                return Result<ChatMessageDto>.Failure(result.Error);

            // Return a minimal DTO — full message details available via GetChatHistory
            return Result<ChatMessageDto>.Success(new ChatMessageDto(
                result.Value,
                request.SenderId,
                request.ReceiverId,
                request.Content,
                "Sent",
                System.DateTime.UtcNow,
                null,
                null));
        }
    }
}
