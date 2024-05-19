using Domain.Responses;
namespace Auth.Routes;

public static class OpenidConfiguration
{
    public static void AddOpenidConfigurationRouter(this WebApplication app)
    {
        app.MapGet("/.well-known/openid-configuration", (IConfiguration configuration) =>
        {
            return configuration.GetSection("OpenidConfiguration").Get<DiscoveryResponse>();
        });
    }
}
