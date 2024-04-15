using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using auth.Models;
using auth.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.AddOpenidConfigurationRoute();

app.MapRazorPages();
app.UseStaticFiles();
app.MapFallbackToPage("/App");

app.Run();
