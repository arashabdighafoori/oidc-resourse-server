using Domain.Common;
using Domain.Request;
using Domain.Responses;
using Domain.Results;
using Microsoft.AspNetCore.Http;
using Persistence.Interfaces;
using Persistence.Stores;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Persistence.Services;

public class AuthorizaionService : IAuthorizaionService
{

    private static string keyAlg = "66007d41-6924-49f2-ac0c-e63c4b1a1730";
    private readonly ICodeService _codeService;
    private readonly IClientStore _clientStore;

    public AuthorizaionService(ICodeService codeService, IClientStore clientStore)
    {
        _codeService = codeService;
        _clientStore = clientStore;
    }
    public async Task<AuthorizeResponse> AuthorizeRequestAsync(IHttpContextAccessor httpContextAccessor, AuthorizationRequest authorizationRequest, CancellationToken cancellationToken)
    {
        AuthorizeResponse response = new AuthorizeResponse();

        if (httpContextAccessor == null)
        {
            response.Error = ErrorTypeEnum.ServerError.GetEnumDescription();
            return response;
        }

        var client = await VerifyClientByIdAsync(authorizationRequest.client_id, cancellationToken);
        if (!client.IsSuccess)
        {
            response.Error = client.ErrorDescription;
            return response;
        }

        if (string.IsNullOrEmpty(authorizationRequest.response_type) || authorizationRequest.response_type != "code")
        {
            response.Error = ErrorTypeEnum.InvalidRequest.GetEnumDescription();
            response.ErrorDescription = "response_type is required or is not valid";
            return response;
        }

        if (!authorizationRequest.redirect_uri.IsRedirectUriStartWithHttps() && !httpContextAccessor.HttpContext.Request.IsHttps)
        {
            response.Error = ErrorTypeEnum.InvalidRequest.GetEnumDescription();
            response.ErrorDescription = "redirect_url is not secure, MUST be TLS";
            return response;
        }


        // check the return url is match the one that in the client store


        // check the scope in the client store with the
        // one that is comming from the request MUST be matched at leaset one

        var scopes = authorizationRequest.scope.Split(' ');

        var clientScopes = from m in client.Client.AllowedScopes
                           where scopes.Contains(m)
                           select m;

        if (!clientScopes.Any())
        {
            response.Error = ErrorTypeEnum.InValidScope.GetEnumDescription();
            response.ErrorDescription = "scopes are invalids";
            return response;
        }

        string nonce = httpContextAccessor.HttpContext.Request.Query["nonce"].ToString();

        // Verify that a scope parameter is present and contains the openid scope value.
        // (If no openid scope value is present,
        // the request may still be a valid OAuth 2.0 request, but is not an OpenID Connect request.)

        string code = await _codeService.GenerateAuthorizationCodeAsync(authorizationRequest.client_id, clientScopes.ToList(), cancellationToken);
        if (code == null)
        {
            response.Error = ErrorTypeEnum.TemporarilyUnAvailable.GetEnumDescription();
            return response;
        }

        response.RedirectUri = client.Client.RedirectUri + "?response_type=code" + "&state=" + authorizationRequest.state;
        response.Code = code;
        response.State = authorizationRequest.state;
        response.RequestedScopes = clientScopes.ToList();
        response.Nonce = nonce;

        return response;

    }

    private async Task<CheckClientResult> VerifyClientByIdAsync(string clientId, CancellationToken cancellationToken, bool checkWithSecret = false, string clientSecret = null)
    {
        CheckClientResult result = new CheckClientResult() { IsSuccess = false };

        if (!string.IsNullOrWhiteSpace(clientId))
        {
            var client = await _clientStore.GetAsync(clientId, cancellationToken);

            if (client != null)
            {
                if (checkWithSecret && !string.IsNullOrEmpty(clientSecret))
                {
                    bool hasSamesecretId = client.ClientSecret.Equals(clientSecret, StringComparison.InvariantCulture);
                    if (!hasSamesecretId)
                    {
                        result.Error = ErrorTypeEnum.InvalidClient.GetEnumDescription();
                        return result;
                    }
                }


                // check if client is enabled or not

                if (client.IsActive)
                {
                    result.IsSuccess = true;
                    result.Client = client;

                    return result;
                }
                else
                {
                    result.ErrorDescription = ErrorTypeEnum.UnAuthoriazedClient.GetEnumDescription();
                    return result;
                }
            }
        }

        result.ErrorDescription = ErrorTypeEnum.AccessDenied.GetEnumDescription();
        return result;
    }
}