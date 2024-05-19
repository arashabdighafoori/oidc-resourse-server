using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Security.Claims;

namespace Domain.Models;

public enum TokenTypeEnum : byte
{
    [Description("Bearer")]
    Bearer
}