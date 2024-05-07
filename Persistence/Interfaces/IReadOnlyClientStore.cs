using Domain.Models;

namespace Persistence.Interfaces
{
    public interface IReadOnlyClientStore
    {
        Task<Client> GetAsync(string clientId, CancellationToken cancellationToken);
    }
}