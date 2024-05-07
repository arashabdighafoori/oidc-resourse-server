using Domain.Models;

namespace Persistence.Interfaces
{
    public interface IClientStore : IReadOnlyClientStore
    {
        Task<bool> DeleteAsync(string clientId, CancellationToken cancellationToken);
        Task<bool> InsertAsync(Client client, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(string clientId, Client client, CancellationToken cancellationToken);
    }
}