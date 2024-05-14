using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Domain.Models;

public class ApplicationUser : MongoUser
{
    public string Friendlyname { get; set; }
    public string Fingerprint { get; set; }
    public string[] FingerprintRecords { get; set; }
}