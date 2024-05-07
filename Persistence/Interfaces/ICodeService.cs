using Domain.Models;

namespace Persistence.Interfaces
{
    public interface ICodeService
    {
        Task<string?> GenerateAuthorizationCodeAsync(string clientId, IList<string> requestedScope, CancellationToken cancellationToken);
        Task<AuthorizationCode?> GetClientDataByCode(string key, CancellationToken cancellationToken);
        Task<AuthorizationCode?> RemoveClientDataByCodeAsync(string key, CancellationToken cancellationToken);
    }
}