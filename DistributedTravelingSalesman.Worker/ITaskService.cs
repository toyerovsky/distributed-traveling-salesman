using DistributedTravelingSalesman.Domain.Entities;
using DistributedTravelingSalesman.Dto;

namespace DistributedTravelingSalesman.Worker
{
    public interface ITaskService
    {
        void StartTask(Graph graph);

        FindBestPartialResultResponseDto FindBestPartialResult(FindBestPartialResultRequestDto request);

        void CancelCurrentTask();
    }
}