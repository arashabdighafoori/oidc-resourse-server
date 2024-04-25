using auth.Models;

namespace auth.Services
{
    public interface IClientStore
    {
        Task<Client> GetAsync(string clientId, CancellationToken cancellationToken);
        Task<bool> InsertAsync(Client client, CancellationToken cancellationToken);
    }
}