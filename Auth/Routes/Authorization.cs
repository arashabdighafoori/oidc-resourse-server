using Domain.Models;
using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;

namespace Auth.Routes;

public static class Authoriztion
{
    private static readonly string Prefix = "/api/v1/auth";
    public static void AddAuthorizationRouter(this WebApplication app)
    {
        AddIsUserRoute(app);
        AddRegisterRoute(app);
        AddAuthorizeRoute(app);
    }

    public static void AddAuthorizeRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/authorize-client", async (ClientAuthorizationRequest authorizationRequest, IHttpContextAccessor httpContextAccessor, IAuthorizaionService authorizaionService, CancellationToken ct) =>
        {
            var result = await authorizaionService.AuthorizeRequestAsync(httpContextAccessor, authorizationRequest, ct);

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

    public static void AddIsUserRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/isuser", async (IsUserRequest isUserRequest, UserManager<ApplicationUser> userManager, CancellationToken ct) =>
        {
            var user = await userManager.FindByEmailAsync(isUserRequest.username);
            return Results.Json(new { ok = true, is_user = user is not null, userName = user.Email });
        });
    }


    public static void AddRegisterRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/register", async (RegisterRequest registerRequest, UserManager<ApplicationUser> userManager, CancellationToken ct) =>
        {
            var result = await userManager.CreateAsync(new()
            {
                Email = registerRequest.Email,
                Fingerprint = registerRequest.Fingerprint,
                FingerprintRecords = new[] { registerRequest.Fingerprint },
                Friendlyname = registerRequest.Friendlyname,
                UserName = registerRequest.Email
            });

            return Results.Json(new { ok = result.Succeeded });
        });
    }

    public static void AddLoginRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/login", async (LoginRequest loginRequest, UserManager<ApplicationUser> userManager, CancellationToken ct) =>
        {
            var result = await userManager.FindByEmailAsync(loginRequest.UserName);

            return Results.Json(new { ok = true });
        });
    }
}
