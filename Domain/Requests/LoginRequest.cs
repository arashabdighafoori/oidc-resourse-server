﻿namespace Domain.Request;
public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string RedirectUri { get; set; }
    public string Code { get; set; }
    public string Nonce { get; set; }
    public IList<string> RequestedScopes { get; set; }
}