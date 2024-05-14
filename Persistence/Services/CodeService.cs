using Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Persistence.Interfaces;
using System.Security.Claims;

namespace Persistence.Services;

public class CodeService : ICodeService
{
    public CodeService(ICodeStore codeStore, IReadOnlyClientStore clientStore)
    {
        this._clientStore = clientStore;
        this._codeStore = codeStore;
    }

    private readonly IReadOnlyClientStore _clientStore;
    private readonly ICodeStore _codeStore;

    public async Task<string?> GenerateAuthorizationCodeAsync(string clientId, IList<string> requestedScope, CancellationToken cancellationToken)
    {
        var client = await _clientStore.GetAsync(clientId, cancellationToken);

        if (client == null)
            return null;

        var code = Guid.NewGuid().ToString();

        var authoCode = new AuthorizationCode
        {
            Code = code,
            ClientId = clientId,
            RedirectUri = client.RedirectUri,
            RequestedScopes = requestedScope,
        };

        // then store the code is the Concurrent Dictionary
        var isSuccess = await _codeStore.InsertAsync(authoCode, cancellationToken);

        if (!isSuccess)
            return null;

        return code;

    }

    public async Task<AuthorizationCode?> GetClientDataByCode(string key, CancellationToken cancellationToken)
    {
        var code = await _codeStore.GetAsync(key, cancellationToken);

        if (code != null)
            return code;

        return null;
    }

    public async Task<AuthorizationCode?> RemoveClientDataByCodeAsync(string key, CancellationToken cancellationToken)
    {
        var code = await _codeStore.GetAsync(key, cancellationToken);

        if (code != null)
            return code;

        var isSuccess = await _codeStore.DeleteAsync(key, cancellationToken);

        if (isSuccess)
            return code;

        return null;
    }


    public async Task<AuthorizationCode> UpdatedClientDataByCodeAsync(string key, IList<string> requestdScopes,
            string userName, CancellationToken cancellationToken, string password = null, string nonce = null)
    {
        var oldValue = await GetClientDataByCode(key, cancellationToken);

        if (oldValue != null)
        {
            // check the requested scopes with the one that are stored in the Client Store 
            var client = await _clientStore.GetAsync(oldValue.ClientId, cancellationToken);

            if (client != null)
            {
                var clientScope = (from m in client.AllowedScopes
                                   where requestdScopes.Contains(m)
                                   select m).ToList();

                if (!clientScope.Any())
                    return null;

                AuthorizationCode newValue = new AuthorizationCode
                {
                    ClientId = oldValue.ClientId,
                    CreationTime = oldValue.CreationTime,
                    IsOpenId = requestdScopes.Contains("openId") || requestdScopes.Contains("profile"),
                    RedirectUri = oldValue.RedirectUri,
                    RequestedScopes = requestdScopes,
                    Nonce = nonce
                };


                var claims = new List<Claim>();
                if (newValue.IsOpenId)
                {
                    // TODO
                    // Add more claims to the claims

                }

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                newValue.Subject = new ClaimsPrincipal(claimIdentity);

                var result = await _codeStore.UpdateAsync(key, newValue, cancellationToken);

                if (result)
                    return newValue;
            }
        }
        return null;
    }
}
