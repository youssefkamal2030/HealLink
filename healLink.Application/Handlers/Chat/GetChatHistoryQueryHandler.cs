using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Queries.Chat;
using HealLink.Contracts.Chat.Responses;
using MediatR;

namespace healLink.Application.Handlers.Chat
{
    public class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryQuery, Result<ChatHistoryResponse>>
    {
        private readonly IChatService _chatService;

        public GetChatHistoryQueryHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<Result<ChatHistoryResponse>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _chatService.ValidateConnection(request.UserId1, request.UserId2);
            if (!validationResult.IsSuccess)
            {
                return Result<ChatHistoryResponse>.Failure(validationResult.Error);
            }

            var chatHistoryResult = await _chatService.GetChatHistoryAsync(request.UserId1, request.UserId2);
            
            if (!chatHistoryResult.IsSuccess)
            {
                return Result<ChatHistoryResponse>.Failure(chatHistoryResult.Error);
            }

            var messages = chatHistoryResult.Value;
            var messageDtos = messages.Select(m => new ChatMessageDto(
                Id: m.Id,
                SenderId: m.SenderId,
                ReceiverId: m.ReceiverId,
                Content: m.Content,
                Status: m.Status.ToString(),
                CreatedAt: m.CreatedAt,
                DeliveredAt: m.DeliveredAt,
                ReadAt: m.ReadAt
            )).ToList();

            var response = new ChatHistoryResponse(
                Success: true,
                Message: "Chat history retrieved successfully.",
                Messages: messageDtos,
                TotalCount: messageDtos.Count
            );

            return Result<ChatHistoryResponse>.Success(response);
        }
    }
}
