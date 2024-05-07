using Domain.Request;
using Domain.Responses;
using Microsoft.AspNetCore.Http;

namespace Persistence.Interfaces
{
    public interface IAuthorizaionService
    {
        Task<AuthorizeResponse> AuthorizeRequestAsync(IHttpContextAccessor httpContextAccessor, AuthorizationRequest authorizationRequest, CancellationToken cancellationToken);
    }
}