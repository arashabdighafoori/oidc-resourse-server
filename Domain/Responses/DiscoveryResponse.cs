﻿namespace Domain.Responses;

public class DiscoveryResponse
{
    public string issuer { get; set; } = null!;
    public string authorization_endpoint { get; set; } = null!;
    public string token_endpoint { get; set; } = null!;
    public IList<string> token_endpoint_auth_methods_supported { get; set; } = null!;
    public IList<string> token_endpoint_auth_signing_alg_values_supported { get; set; } = null!;
    public string userinfo_endpoint { get; set; } = null!;
    public string check_session_iframe { get; set; } = null!;
    public string end_session_endpoint { get; set; } = null!;
    public string jwks_uri { get; set; } = null!;
    public string registration_endpoint { get; set; } = null!;
    public IList<string> scopes_supported { get; set; } = null!;
    public IList<string> response_types_supported { get; set; } = null!;
    public IList<string> acr_values_supported { get; set; } = null!;
    public IList<string> subject_types_supported { get; set; } = null!;
    public IList<string> userinfo_signing_alg_values_supported { get; set; } = null!;
    public IList<string> userinfo_encryption_alg_values_supported { get; set; } = null!;
    public IList<string> userinfo_encryption_enc_values_supported { get; set; } = null!;
    public IList<string> id_token_signing_alg_values_supported { get; set; } = null!;
    public IList<string> id_token_encryption_alg_values_supported { get; set; } = null!;
    public IList<string> id_token_encryption_enc_values_supported { get; set; } = null!;
    public IList<string> request_object_signing_alg_values_supported { get; set; } = null!;
    public IList<string> display_values_supported { get; set; } = null!;
    public IList<string> claim_types_supported { get; set; } = null!;
    public IList<string> claims_supported { get; set; } = null!;
    public bool claims_parameter_supported { get; set; }
    public string service_documentation { get; set; } = null!;
    public IList<string> ui_locales_supported { get; set; } = null!;
}
