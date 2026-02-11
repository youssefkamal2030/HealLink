namespace HealLink.Contracts.Connections.Responses
{
    public record CreateConnectionRequestResponse(
        Guid ConnectionRequestId,
        string Status
    );

}