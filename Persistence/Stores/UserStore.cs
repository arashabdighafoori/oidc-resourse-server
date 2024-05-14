using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Persistence.Stores;

public class UserStore : GenericStore<Client>, IClientStore
{
    public UserStore(IConfiguration configuration) : base(configuration)
    {
        Initialize("dojo", "users");
    }

    public async Task<Client> GetAsync(string clientId, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<Client>.Filter.Eq(e => e.ClientId, clientId);
            var client = await Collection.FindAsync(filter, new(), cancellationToken);
            return await client.FirstAsync(cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> UpdateAsync(string clientId, Client client, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<Client>.Filter.Eq(e => e.ClientId, clientId);
            var result = await Collection.ReplaceOneAsync(filter, client, new ReplaceOptions() { }, cancellationToken);

            return result.IsAcknowledged;
        }
        catch (Exception)
        {

            throw;
        }
    }


    public async Task<bool> DeleteAsync(string clientId, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<Client>.Filter.Eq(e => e.ClientId, clientId);
            var result = await Collection.DeleteOneAsync(filter, new(), cancellationToken);
            return result.IsAcknowledged;
        }
        catch (Exception)
        {

            throw;
        }
    }
}