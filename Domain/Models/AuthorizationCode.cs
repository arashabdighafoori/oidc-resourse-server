using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace Domain.Models;

[DataContract]
[BsonIgnoreExtraElements]
public class AuthorizationCode
{
    public string Code { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RedirectUri { get; set; }

    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public bool IsOpenId { get; set; }
    public IList<string> RequestedScopes { get; set; }

    public ClaimsPrincipal Subject { get; set; }
    public string Nonce { get; set; }
}