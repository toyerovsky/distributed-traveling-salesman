using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using DistributedTravelingSalesman.Domain.Entities;
using DistributedTravelingSalesman.Dto;
using Microsoft.Extensions.Logging;

namespace DistributedTravelingSalesman.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly ILogger<IWorkerService> _logger;

        private readonly ConcurrentDictionary<string, Worker> _registeredWorkers = new();

        public WorkerService(ILogger<IWorkerService> logger)
        {
            _logger = logger;
        }

        public Task<GetOnlineWorkersResponseDto> GetOnlineWorkers()
        {
            return Task.FromResult(new GetOnlineWorkersResponseDto
            {
                Data = _registeredWorkers.Select(x => new GetOnlineWorkersResponseDto.ListItem
                {
                    Id = x.Value.Id,
                    Url = x.Value.Url
                })
            });
        }

        public Task AddWorker(AddWorkerDto request)
        {
            if (_registeredWorkers.ContainsKey(request.Url))
                throw new InvalidOperationException($"Worker with given url ({request.Url}) already exists");

            var worker = Worker.Create(request.Url);
            _registeredWorkers.TryAdd(worker.Url, worker);
            _logger.LogInformation($"Registered {request.Url} worker");

            return Task.CompletedTask;
        }

        public Task RemoveWorker(RemoveWorkerDto request)
        {
            if (_registeredWorkers.ContainsKey(request.Url) == false)
                throw new InvalidOperationException($"Worker with given url ({request.Url}) does not exist");
            _registeredWorkers.TryRemove(request.Url, out _);
            _logger.LogInformation($"Deregistered {request.Url} worker");

            return Task.CompletedTask;
        }
    }
}