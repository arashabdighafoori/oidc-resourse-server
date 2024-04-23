using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using auth.Models;
using auth.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.AddPagesRoute();
app.AddOpenidConfigurationRoute();

app.MapGet("/test", async () => {

    var client = new MongoClient("mongodb://localhost:27017");
    var database = client.GetDatabase("foo");
    var collection = database.GetCollection<BsonDocument>("bar");

    await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

    var list = await collection.Find(new BsonDocument("Name", "Jack"))
        .ToListAsync();

    foreach (var document in list)
    {
        Console.WriteLine(document["Name"]);
    }
    return new { ok = true };
});


app.Run();
