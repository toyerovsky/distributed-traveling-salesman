using System.Text.Json;
using DistributedTravelingSalesman.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DistributedTravelingSalesman.Worker.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost("begin")]
        public IActionResult Begin(BeginTaskRequestDto request)
        {
            _logger.LogInformation("Begin, graph: {}", JsonSerializer.Serialize(request));

            _taskService.StartTask(request.Graph);

            return NoContent();
        }

        [HttpPost("findBestPartialResult")]
        public IActionResult FindBestPartialResult(FindBestPartialResultRequestDto request)
        {
            return Ok(_taskService.FindBestPartialResult(request));
        }

        [HttpDelete]
        public IActionResult Cancel()
        {
            _taskService.CancelCurrentTask();
            return NoContent();
        }
    }
}