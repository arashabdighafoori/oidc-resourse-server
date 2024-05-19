using Domain.Models;

namespace Persistence.Interfaces
{
    public interface ICodeService
    {
        Task<string?> GenerateAuthorizationCodeAsync(string clientId, IList<string> requestedScope, CancellationToken cancellationToken);
        Task<AuthorizationCode?> GetClientDataByCode(string key, CancellationToken cancellationToken);
        Task<AuthorizationCode?> RemoveClientDataByCodeAsync(string key, CancellationToken cancellationToken);
        Task<AuthorizationCode> UpdatedClientDataByCodeAsync(string key, IList<string> requestdScopes, string userName, CancellationToken cancellationToken, string password = null, string nonce = null);
    }
}