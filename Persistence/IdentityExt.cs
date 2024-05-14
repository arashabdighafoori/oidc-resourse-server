using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;
using Persistence.Stores;
using System.Runtime.CompilerServices;

namespace Persistence;

public static class IdentityExt
{

    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityMongoDbProvider<ApplicationUser, MongoRole>(identity =>
        {
        },
        mongo =>
        {
            mongo.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            mongo.MigrationCollection = "migrations";
            mongo.RolesCollection = "roles";
            mongo.UsersCollection = "users";
        });
    }
}
