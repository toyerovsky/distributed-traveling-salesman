using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace DistributedTravelingSalesman.Worker
{
    public class RegisterService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<RegisterService> _logger;

        public RegisterService(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<RegisterService> logger, IConfiguration configuration)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
            _configuration = configuration;
        }

        public static IServerAddressesFeature Addresses { get; set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deregistering worker");
            var client = new RestClient(_configuration["RegisterUrl"]);
            var request = new RestRequest();
            request.AddOrUpdateParameter("url", await GetUrl());
            var response = await client.DeleteAsync(request);
            if (!response.IsSuccessful)
                _logger?.LogError($"Could not deregister worker: {response.StatusCode} {response.StatusDescription}");
            else
                _logger?.LogInformation("Successfully deregistered!");
        }

        private async Task<string> GetUrl()
        {
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());

            var ip = host
                .AddressList
                .FirstOrDefault(ip =>
                    ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith("192"));

            return FixWildcard($"http://{ip}:{GetPort(Addresses.Addresses.First())}");
        }

        private async void OnStarted()
        {
            var hostInfo = new Domain.Entities.Worker
            {
                Url = await GetUrl()
            };
            var client = new RestClient(_configuration["RegisterUrl"]);
            var request = new RestRequest();
            request.AddJsonBody(hostInfo);
            var response = await client.PostAsync(request);
            if (!response.IsSuccessful)
                _logger?.LogError($"Could not register worker: {response.StatusCode} {response.StatusDescription}");
            else
                _logger?.LogInformation("Successfully registered!");
        }

        private string FixWildcard(string address)
        {
            return address.Replace("[::]", "localhost");
        }

        private string GetPort(string address)
        {
            var parts = address.Replace("[::]", "").Split(':');

            return parts[2];
        }
    }
}