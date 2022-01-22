using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DistributedTravelingSalesman.Domain.Entities;
using DistributedTravelingSalesman.Dto;

namespace DistributedTravelingSalesman.Service
{
    public class GraphService : IGraphService
    {
        public Stream GenerateGraph(int nodesCount)
        {
            var graphGenerator = new GraphGenerator();
            var graph = graphGenerator.GenerateGraph(nodesCount);
            var jsonGraph = JsonSerializer.Serialize(graph);
            var byteArray = Encoding.ASCII.GetBytes(jsonGraph);
            var stream = new MemoryStream(byteArray);
            return stream;
        }

        public async Task<GetBestHamiltonianPathResponseDto> GetBestHamiltonianPath(IList<Worker> workers,
            GetBestHamiltonianPathRequestDto request)
        {
            var graph = request.Graph;

            var helpersTasks = workers
                .Select(e => WorkerConnectionHelper.Create(e, graph))
                .ToArray();

            var helpers = await Task.WhenAll(helpersTasks);

            if (helpers.Length == 0) throw new InvalidOperationException("No available workers");

            var startNode = request.StartIndex;
            var nodesCount = graph.AdjMatrix.GetLength(0);

            var chunkSize = (int)Math.Floor((double)(nodesCount - 1) / helpers.Length);

            var chunks = new List<int>();
            var helperCounter = 0;
            var partialTasks = new List<Task<FindBestPartialResultResponseDto>>();

            for (var i = 0; i < nodesCount; ++i)
            {
                if (i == startNode) continue;

                if (chunks.Count != 0 && chunks.Count % chunkSize == 0)
                {
                    partialTasks.Add(helpers[helperCounter++].CalculateFor(startNode, chunks));
                    chunks.Clear();
                }

                chunks.Add(i);
            }

            if (chunks.Count > 0) partialTasks.Add(helpers[helperCounter].CalculateFor(startNode, chunks));

            var partialResults = await Task.WhenAll(partialTasks);
            var bestResult = partialResults.OrderBy(x => x.Route).First();

            return new GetBestHamiltonianPathResponseDto
            {
                TotalRoute = bestResult.Route,
                Indexes = bestResult.Nodes
            };
        }
    }
}