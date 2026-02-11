namespace HealLink.Contracts.Notifications
{
    public record GetAllNotificationsRequest(Guid UserId, string UserType);

}