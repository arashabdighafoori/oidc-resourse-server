using Auth.Routes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Persistence;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFeatureManagement(builder.Configuration.GetSection("Features"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddStores();
builder.Services.AddServices();
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => { });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/auth";
    options.LoginPath = "/auth";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.AddFeatureRouter();
app.AddAuthorizationRouter();
app.AddOpenidConfigurationRouter();
app.AddPagesRouter();

app.MapGet("/test", [Authorize] () => "Hello World");


app.Run();
