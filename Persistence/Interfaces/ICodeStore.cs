using Domain.Models;

namespace Persistence.Interfaces
{
    public interface ICodeStore : IReadOnlyCodeStore
    {
        Task<bool> DeleteAsync(string code, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(string codeId, AuthorizationCode code, CancellationToken cancellationToken);
        Task<bool> InsertAsync(AuthorizationCode code, CancellationToken cancellationToken);
    }
}