using auth.Routes;
using auth.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStores();

var app = builder.Build();

app.UseStaticFiles();
app.AddPagesRoute();
app.AddOpenidConfigurationRoute();

app.Run();
