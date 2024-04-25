using auth.Models;
using auth.Services;
using System.Runtime.CompilerServices;

namespace auth.Stores;

public static class StoresExt
{

    public static void AddStores(this IServiceCollection services)
    {
        services.AddScoped<IClientStore, ClientStore>();
    }
}
