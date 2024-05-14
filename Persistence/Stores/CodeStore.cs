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

    public async Task<bool> UpdateAsync(string codeId, AuthorizationCode code, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<AuthorizationCode>.Filter.Eq(e => e.Code, codeId);
            var update = Builders<AuthorizationCode>.Update
                .Set(e => e.ClientId, code.ClientId)
                .Set(e => e.CreationTime, code.CreationTime)
                .Set(e => e.IsOpenId, code.RequestedScopes.Contains("openId") || code.RequestedScopes.Contains("profile"))
                .Set(e => e.RedirectUri, code.RedirectUri)
                .Set(e => e.RequestedScopes, code.RequestedScopes)
                .Set(e => e.Nonce, code.Nonce);
            var result = await Collection.UpdateOneAsync(filter, update, new(), cancellationToken);
            return result.IsAcknowledged;
        }
        catch (Exception)
        {

            throw;
        }
    }
}