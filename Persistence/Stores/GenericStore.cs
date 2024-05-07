using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Persistence.Stores;

public class GenericStore<T>
{
    private readonly MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<T> _collection;
    protected IMongoCollection<T> Collection
    {
        get { return _collection; }
    }


    public GenericStore(IConfiguration configuration)
    {
        client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
    }

    public void Initialize(string db, string collection)
    {
        database = client.GetDatabase(db);
        _collection = database.GetCollection<T>(collection);
    }

    public async Task<bool> InsertAsync(T client, CancellationToken cancellationToken)
    {
        try
        {
            await Collection.InsertOneAsync(client, new() { }, cancellationToken);
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}