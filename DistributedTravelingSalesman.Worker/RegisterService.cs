using System.Linq;
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
            _hostApplicationLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void OnStarted()
        {
            var hostInfo = new Domain.Entities.Worker
            {
                Url = FixWildcard(Addresses.Addresses.First())
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

        private async void OnStopped()
        {
            var client = new RestClient(_configuration["RegisterUrl"]);
            var request = new RestRequest();
            request.AddOrUpdateParameter("uri", FixWildcard(Addresses.Addresses.First()));
            var response = await client.PostAsync(request);
            if (!response.IsSuccessful)
                _logger?.LogError($"Could not deregister worker: {response.StatusCode} {response.StatusDescription}");
            else
                _logger?.LogInformation("Successfully deregistered!");
        }

        private string FixWildcard(string address)
        {
            return address.Replace("[::]", "localhost");
        }
    }
}