using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;
using Persistence.Services;
using System.Net;

namespace Auth.Routes;

public static class Authoriztion
{

    private static string? Index { get; set; }
    public static void AddAuthorizationRouter(this WebApplication app)
    {
        AddAuthorizeRoute(app);
    }

    public static void AddAuthorizeRoute(WebApplication app)
    {
        app.MapGet("/authorize", async ([FromBody] AuthorizationRequest authorizationRequest, [FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] IAuthorizaionService authorizaionService, CancellationToken c) =>
        {
            var result = await authorizaionService.AuthorizeRequestAsync(httpContextAccessor, authorizationRequest, c);

            if (result.HasError)
                return Results.RedirectToRoute("Error", new { error = result.Error });

            var loginModel = new LoginRequest
            {
                RedirectUri = result.RedirectUri,
                Code = result.Code,
                RequestedScopes = result.RequestedScopes,
                Nonce = result.Nonce
            };

            return Results.Json(loginModel);
        });
    }
}
