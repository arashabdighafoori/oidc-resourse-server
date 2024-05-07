using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;
using Persistence.Services;
using Persistence.Stores;
using System.Runtime.CompilerServices;

namespace Persistence;

public static class ServicesExt
{

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICodeService, CodeService>();
        services.AddScoped<IAuthorizaionService, AuthorizaionService>();
    }
}
