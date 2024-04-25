using auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace auth.Services;

public class GenericStore<T>
{
    private readonly MongoClient client;
    private IMongoDatabase database;

    private IMongoCollection<T> _collection;
    protected IMongoCollection<T> Collection { 
        get { return _collection; }
    }


    public GenericStore(IConfiguration configuration)
    {
        this.client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
    }

    public void Initialize(string db, string collection)
    {
        this.database = client.GetDatabase(db);
        this._collection = database.GetCollection<T>(collection);
    }

    public async Task<bool> InsertAsync(T client, CancellationToken cancellationToken)
    {
        try
        {
            await this.Collection.InsertOneAsync(client, new() { }, cancellationToken);
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}