using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;
using Persistence.Stores;
using System.Runtime.CompilerServices;

namespace Persistence;

public static class StoresExt
{

    public static void AddStores(this IServiceCollection services)
    {
        services.AddScoped<ICodeStore, CodeStore>();
        services.AddScoped<IReadOnlyCodeStore, CodeStore>();

        services.AddScoped<IClientStore, ClientStore>();
        services.AddScoped<IReadOnlyClientStore, ClientStore>();
    }
}
