using auth.Models;

namespace auth.Services;

public class ClientStore
{
    public IEnumerable<Client> Clients = new[]
    {
            new Client
            {
                ClientName = "platformnet .Net 6",
                ClientId = "platformnet6",
                ClientSecret = "123456789",
                AllowedScopes = new[]{ "openid", "profile"},
                GrantType = GrantTypes.Code,
                IsActive = true,
                ClientUri = "https://localhost:5066",
                RedirectUri = "https://localhost:5066/auth_callback/"
            }
        };
}