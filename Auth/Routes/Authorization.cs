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

public static class Authoriztion
{
    private static readonly string Prefix = "/api/v1/auth";
    public static void AddAuthorizationRouter(this WebApplication app)
    {
        AddIsUserRoute(app);
        AddRegisterRoute(app);
        AddLoginRoute(app);
        AddTokenRoute(app);
        AddAuthorizeRoute(app);
    }

    public static void AddAuthorizeRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/authorize-client", async (ClientAuthorizationRequest authorizationRequest, IHttpContextAccessor httpContextAccessor, IAuthorizaionService authorizaionService, CancellationToken ct) =>
        {
            var (isFingerprint, fingerprint) = authorizationRequest.fingerprint.IsValidString();
            if (fingerprint.StartsWith("@"))
                return Results.Ok(new { ok = false, error = "invalid_request" });

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
            var (isFingerprint, fingerprint) = isUserRequest.Fingerprint.IsValidString();
            if (fingerprint.StartsWith("@"))
                return Results.Ok(new { ok = false, error = "invalid_request" });

            var user = await userManager.FindByEmailAsync(isUserRequest.Username);
            return Results.Json(new { ok = true, is_user = user is not null, userName = user?.Email });
        });
    }

    public static void AddTokenRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/token", async (IHttpContextAccessor httpContextAccessor, IAuthorizaionService authorizaionService, CancellationToken ct) =>
        {
            var result = await authorizaionService.GenerateTokenAsync(httpContextAccessor, ct);

            if (result.HasError)
                return Results.Json("0");

            return Results.Json(result);
        });
    }

    public static void AddLoginRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/login", async (LoginRequest loginRequest, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICodeService codeService, CancellationToken ct) =>
        {
            var (isFingerprint, fingerprint) = loginRequest.Fingerprint.IsValidString();
            if(fingerprint.StartsWith("@"))
                return Results.Ok(new { ok = false, error = "invalid_request" });

            var (isUserNameValid, username) = loginRequest.UserName.IsValidString();

            ApplicationUser? userResult;
            if (isUserNameValid && username.IsValidEmail())
                userResult = await userManager.FindByEmailAsync(loginRequest.UserName);
            else
                // Wrong username string type
                return Results.Ok(new { ok = false, error = "invalid_request" });

            var (isUserValid, user) = userResult.IsNotNull();
            var (isPasswordValid, password) = loginRequest.Password.IsValidString();
            if (isUserValid)
            {
                var passResult = await userManager.CheckPasswordAsync(user, password);
                if (!passResult)
                {
                    // Wrong password
                    isUserValid = false;
                }
            }
            else
            {
                // user not found
            }

            var (isNonceValid, nonce) = loginRequest.Nonce.IsValidString();
            var (isCodeValid, code) = loginRequest.Code.IsValidString();
            var (isRequestedScopesValid, requestedScopes) = loginRequest.RequestedScopes.IsNotNull();
            if (isUserValid && isCodeValid && isRequestedScopesValid && isNonceValid)
            {
                var result = await codeService.UpdatedClientDataByCodeAsync(code, requestedScopes,
                    username, ct, nonce: nonce);
                if (result is not null)
                {
                    //Console.WriteLine(loginRequest.RedirectUri);
                    loginRequest.RedirectUri = loginRequest.RedirectUri + "&code=" + loginRequest.Code;
                    return Results.Ok(new { ok = true, url = loginRequest.RedirectUri, name = user.Friendlyname });
                }
            }
            else if (isUserValid)
            {
                Console.WriteLine("ha?");
                await signInManager.SignInAsync(user, true);
                return Results.Ok(new { ok = true, url = "/", name = user.Friendlyname });
            }
            else
                return Results.Ok(new { ok = false, error = "invalid_request" });
            return Results.RedirectToRoute("Error", new { error = "invalid_request" });
        });
    }


    public static void AddRegisterRoute(WebApplication app)
    {
        app.MapPost(Prefix + "/register", async (RegisterRequest registerRequest, ICodeService codeService, UserManager<ApplicationUser> userManager, CancellationToken ct) =>
        {
            var (isFingerprint, fingerprint) = registerRequest.Fingerprint.IsValidString();
            if (fingerprint.StartsWith("@"))
                return Results.Ok(new { ok = false, error = "invalid_request" });

            var (isEmailValid, email) = registerRequest.Email.IsValidString();
            var (isPasswordValid, password) = registerRequest.Password.IsValidString();

            if (!isPasswordValid || !isEmailValid || !email.IsValidEmail())
            {
                // wrong input
                return Results.Ok(new { ok = false, error = "invalid_request" });

            }
            var registerResult = await userManager.CreateAsync(new()
            {
                Email = registerRequest.Email,
                Fingerprint = registerRequest.Fingerprint,
                FingerprintRecords = new[] { registerRequest.Fingerprint },
                Friendlyname = registerRequest.Friendlyname,
                UserName = registerRequest.Email
            });

            ApplicationUser? userResult;
            userResult = await userManager.FindByEmailAsync(email);
            var (isUserValid, user) = userResult.IsNotNull();
            if(registerResult.Succeeded && isUserValid)
            {
                var passResult = await userManager.AddPasswordAsync(user, password);
                if (!passResult.Succeeded)
                {
                    isUserValid = false;
                }
            }


            var (isNonceValid, nonce) = registerRequest.Nonce.IsValidString();
            var (isCodeValid, code) = registerRequest.Code.IsValidString();
            var (isRequestedScopesValid, requestedScopes) = registerRequest.RequestedScopes.IsNotNull();
            if (isUserValid && isCodeValid && isRequestedScopesValid && isNonceValid)
            {
                var result = await codeService.UpdatedClientDataByCodeAsync(code, requestedScopes,
                    email, ct, nonce: nonce);
                if (result is not null)
                {
                    registerRequest.RedirectUri = registerRequest.RedirectUri + "&code=" + registerRequest.Code;
                    return Results.Ok(new { ok = true, url = registerRequest.RedirectUri, name = user.Friendlyname });
                }
            }

            return Results.Ok(new { ok = false, error = "invalid_request" });
        });
    }

}
