using System.Threading.Tasks;
using DistributedTravelingSalesman.Dto;
using DistributedTravelingSalesman.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistributedTravelingSalesman.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOnlineWorkers()
        {
            return Ok(await _workerService.GetOnlineWorkers());
        }

        [HttpPost]
        public async Task<IActionResult> AddWorker(AddWorkerDto request)
        {
            await _workerService.AddWorker(request);

            return Created("", "");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveWorker([FromQuery] string url)
        {
            await _workerService.RemoveWorker(new RemoveWorkerDto { Url = url });

            return NoContent();
        }
    }
}