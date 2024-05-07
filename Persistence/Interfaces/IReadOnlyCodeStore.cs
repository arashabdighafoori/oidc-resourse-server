using Domain.Models;

namespace Persistence.Interfaces
{
    public interface IReadOnlyCodeStore
    {
        Task<AuthorizationCode> GetAsync(string code, CancellationToken cancellationToken);
    }
}