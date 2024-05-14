namespace Domain.Request;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Friendlyname { get; set; }
    public string Fingerprint { get; set; }
    public string Password { get; set; }
    public string RedirectUri { get; set; }
    public string Code { get; set; }
    public string Nonce { get; set; }
    public IList<string> RequestedScopes { get; set; }
}
