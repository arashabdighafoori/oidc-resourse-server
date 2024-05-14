using Auth.Routes;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddStores(); 
builder.Services.AddServices();
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseStaticFiles();
app.AddAuthorizationRouter();
app.AddOpenidConfigurationRouter();
app.AddPagesRouter();

app.Run();
