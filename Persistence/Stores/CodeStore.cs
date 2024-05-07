using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Persistence.Stores;

public class CodeStore : GenericStore<AuthorizationCode>, ICodeStore
{
    public CodeStore(IConfiguration configuration) : base(configuration)
    {
        Initialize("dojo", "codes");
    }

    public async Task<AuthorizationCode> GetAsync(string code, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<AuthorizationCode>.Filter.Eq(e => e.Code, code);
            var client = await Collection.FindAsync(filter, new(), cancellationToken);
            return await client.FirstAsync(cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> DeleteAsync(string code, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<AuthorizationCode>.Filter.Eq(e => e.Code, code);
            var result = await Collection.DeleteOneAsync(filter, new(), cancellationToken);
            return result.IsAcknowledged;
        }
        catch (Exception)
        {

            throw;
        }
    }
}