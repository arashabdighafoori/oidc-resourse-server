using auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace auth.Services;

public class ClientStore : GenericStore<Client>, IClientStore
{
    public ClientStore(IConfiguration configuration) : base(configuration)
    {
        this.Initialize("dojo", "clients");
    }

    public async Task<Client> GetAsync(string clientId, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<Client>.Filter.Eq("ClientId", clientId);
            var client = await this.Collection.FindAsync(filter, new() ,cancellationToken);
            return await client.FirstAsync(cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }
}