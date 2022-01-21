using System;
using System.Threading.Tasks;
using DistributedTravelingSalesman.Dto;
using DistributedTravelingSalesman.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace DistributedTravelingSalesman.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class GraphController : ControllerBase
    {
        private readonly IGraphService _graphService;
        private readonly IWorkerService _workerService;

        public GraphController(IGraphService graphService, IWorkerService workerService)
        {
            _graphService = graphService;
            _workerService = workerService;
        }

        [HttpPut]
        public async Task<IActionResult> GetBestHamiltonianPath(GetBestHamiltonianPathRequestDto request)
        {
            var workers = await _workerService.GetOnlineWorkers();

            return Ok(await _graphService.GetBestHamiltonianPath(workers.GetWorkerList(), request));
        }

        [HttpGet("generate/{numberOfNodes}")]
        public FileStreamResult GenerateGraph(int? numberOfNodes)
        {
            if (numberOfNodes == null)
                throw new ArgumentNullException(nameof(numberOfNodes));

            var stream = _graphService.GenerateGraph(numberOfNodes.Value);

            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "generatedGraph.json"
            };
        }
    }
}