using Domain.Common;
using Domain.Models;
using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Persistence.Interfaces;
using Persistence.Services;

namespace Auth.Routes;

public static class ClientFeatures
{
    private static readonly string Prefix = "/api/v1/features";
    public static void AddFeatureRouter(this WebApplication app)
    {
        AddFeatureRoute(app);
    }

    public static void AddFeatureRoute(WebApplication app)
    {
        app.MapGet(Prefix, () =>
        {
            return Results.Json(new
            {
                themeView = true,
                languageView = true,
                getCultureFromNavigation = false,
            }) ;
        });
    }
}
