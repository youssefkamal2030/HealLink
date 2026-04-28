using healLink.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
      

        private readonly ICurrentUserService _currentUser;
        private readonly IEnumerable<IAuthorizationPolicy> _policies;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is not IAuthorizeRequest authorizeRequest)
                return await next();

            var policy = _policies.FirstOrDefault(p => p.Name == authorizeRequest.Policy)
                ?? throw new InvalidOperationException(
                    $"Authorization policy '{authorizeRequest.Policy}' is not registered.");

            var authorized = await policy.AuthorizeAsync(_currentUser, request, cancellationToken);
            if (!authorized)
                throw new UnauthorizedAccessException(
                    $"Access denied. Policy '{authorizeRequest.Policy}' was not satisfied.");

            return await next();
        }
    }
}
