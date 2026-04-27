namespace healLink.Application.Interfaces;

public interface IAuthorizationPolicy
{
    string Name { get; }
    Task<bool> AuthorizeAsync(ICurrentUserService currentUser, object request, CancellationToken cancellationToken);
}
