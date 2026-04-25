using System;

namespace HealLink.Contracts.Chat.Requests
{
    public record SendMessageRequest(Guid SenderId, Guid ReceiverId, string Content);
}
