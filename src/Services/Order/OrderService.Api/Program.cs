using OrderService.Infrastructure.Extensions;
using OrderService.OrderAPI.Configuration;
using Prometheus;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);

var app = builder.Build();

app.UseHttpMetrics();
app.MapMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

//app.RegisterWithConsul(app.Lifetime);

app.Run();

//TODO: UnitTest yazýlacak.

//TODO: Consul Ocelot

//TODO: Jenkinsde CI sürecine testler baðlanacak.

//TODO: Circuit Breaker

//TODO: Rate limit

//TODO: EDD

//TODO: Saga Pattern!

//TODO: Loglama monitoring tracing

//TODO: Service Orchestrator

//TODO: Elasticsearch, ELK?, grafana, SEQ, dynatrace, newrelic, appdynamic

//TODO: EventSourcing implemente edilecek. EventStore

//TODO: Security Test

//TODO: NextJs.

//TODO: Authentication and Authorization

//TODO: TDD?

//TODO: Elasticsearch, ELK?, grafana, SEQ, dynatrace, newrelic, appdynamic