using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Domain.Commands;
using OrderService.Domain.Events;
using OrderService.Domain.Interfaces;
using OrderService.Domain.Queries;
using OrderService.Domain.Services;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repositories;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;

namespace OrderService.Infrastructure.IoC
{
    public static class InjectionHelper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("OrderService.Domain")));
            // Application
            services.AddScoped<IOrderAppService, OrderAppService>();

            // Domain Services
            services.AddScoped<IOrderDomainService, OrderDomainService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateOrderCommand, bool>, CreateOrderCommandHandler>();

            // Domain - Queries
            services.AddScoped<IRequestHandler<GetOrderByIdQuery, string>, GetOrderByIdQueryHandler>();

            // Infra - Data
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.ConfigureConsul(configuration);
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            //servis discovery, health check config
            services.Configure<ConsulConfig>(configuration.GetSection("ConsulConfig"));

            //hostedservices
            services.AddHostedService<ConsulHostedService>();

            services.AddSingleton<AsyncCircuitBreakerPolicy>(serviceProvider =>
            {
                return CircuitPolicy.CreatePolicy(1, TimeSpan.FromSeconds(30));
            });

            //health check
            services.AddHealthChecks();
        }
    }
}
