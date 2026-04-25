using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Chat.Responses;
using MediatR;

namespace healLink.Application.Commands.Chat
{
    public record SendMessageCommand(
        Guid SenderId,
        Guid ReceiverId,
        string Content
    ) : IRequest<Result<ChatMessageDto>>;

    public record MarkAsDeliveredCommand(Guid MessageId) : IRequest<Result<bool>>;

    public record MarkAsReadCommand(Guid MessageId) : IRequest<Result<bool>>;
}
