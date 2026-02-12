
namespace HealLink.Contracts.Chat.Responses
{
    public record ChatMessageDto(
        Guid Id,
        Guid SenderId,
        Guid ReceiverId,
        string Content,
        string Status,
        DateTime CreatedAt,
        DateTime? DeliveredAt,
        DateTime? ReadAt
    );

    public record ChatHistoryResponse(
        bool Success,
        string Message,
        List<ChatMessageDto> Messages,
        int TotalCount
    );
}
