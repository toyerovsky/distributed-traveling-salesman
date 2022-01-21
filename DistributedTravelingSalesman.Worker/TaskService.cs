using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DistributedTravelingSalesman.Domain.Entities;
using DistributedTravelingSalesman.Dto;
using Microsoft.Extensions.Logging;

namespace DistributedTravelingSalesman.Worker
{
    public class TaskService : ITaskService
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ILogger<TaskService> _logger;
        private Graph _currentGraph;
        private Task _task;

        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }

        public void StartTask(Graph graph)
        {
            _currentGraph = graph;
        }

        public void CancelCurrentTask()
        {
            _cts.Cancel();
        }

        public FindBestPartialResultResponseDto FindBestPartialResult(FindBestPartialResultRequestDto request)
        {
            var bruteforceSolver = new BruteforceSolver();
            var results = new List<SolverResult>();

            _logger.LogInformation("Request: {}", JsonSerializer.Serialize(request));

            foreach (var chunk in request.Chunks)
            {
                results.Add(bruteforceSolver.Solve(_currentGraph, request.StartIndex, chunk));
                bruteforceSolver.Clear();
            }

            var bestChunk = results.OrderBy(x => x.Route).First();

            var response = new FindBestPartialResultResponseDto
            {
                Nodes = bestChunk.Nodes,
                Route = bestChunk.Route
            };

            _logger.LogInformation("Results: {}", JsonSerializer.Serialize(response));

            return response;
        }
    }
}