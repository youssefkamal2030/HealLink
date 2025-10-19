namespace healLink.Application.Handlers.Connection
{
    public record CreateConnectionRequestResponse(
        Guid ConnectionRequestId,
        string Status
    );

}