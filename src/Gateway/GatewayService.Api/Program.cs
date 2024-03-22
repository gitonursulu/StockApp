using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.MetadataAddress = "http://localhost:5053/identity/realms/secured/.well-known/openid-configuration";
        o.RequireHttpsMetadata = false;
        o.Authority = "http://localhost:5053/realms/secured";
        o.Audience = "account";
    });

builder.Services.AddOcelot(builder.Configuration).AddConsul();
builder.Configuration.AddJsonFile("ocelot.json");

//hostedservices

//health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

app.MapHealthChecks("/health");

app.Run();
