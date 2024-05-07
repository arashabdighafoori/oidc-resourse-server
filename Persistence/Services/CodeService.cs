using Domain.Models;
using Persistence.Interfaces;

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


}
