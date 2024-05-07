using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Models;

[DataContract]
[BsonIgnoreExtraElements]
public class Client
{

    public string ClientName { get; set; }
    public string ClientId { get; set; }

    /// <summary>
    /// Client Password
    /// </summary>
    public string ClientSecret { get; set; }

    public IList<string> GrantType { get; set; }

    /// <summary>
    /// by default false
    /// </summary>
    public bool IsActive { get; set; } = false;
    public IList<string> AllowedScopes { get; set; }

    public string ClientUri { get; set; }
    public string RedirectUri { get; set; }
}