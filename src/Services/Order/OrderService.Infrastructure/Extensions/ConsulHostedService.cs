    using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Extensions
{
    public class ConsulConfig
    {
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public int ServicePort { get; set; }
    }

    public class ConsulHostedService : IHostedService
    {
        private readonly IConsulClient _consulClient;
        private readonly IOptions<ConsulConfig> _consulConfig;
        private string _registrationID;

        public ConsulHostedService(IConsulClient consulClient, IOptions<ConsulConfig> consulConfig)
        {
            _consulClient = consulClient;
            _consulConfig = consulConfig;
            _registrationID = $"{_consulConfig.Value.ServiceID}-{Guid.NewGuid()}";
        }

        //when application starting
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var registration = new AgentServiceRegistration()
            {
                ID = _registrationID,
                Name = _consulConfig.Value.ServiceName,
                Address = _consulConfig.Value.ServiceAddress,
                Port = _consulConfig.Value.ServicePort,
                Tags = new[] { "order", "api" },

                Check = new AgentServiceCheck()
                {
                    HTTP = $"http://{_consulConfig.Value.ServiceAddress}:{_consulConfig.Value.ServicePort}/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1) 
                }
            };

            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        //when application stopping
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _consulClient.Agent.ServiceDeregister(_registrationID, cancellationToken);
        }

    }
}
