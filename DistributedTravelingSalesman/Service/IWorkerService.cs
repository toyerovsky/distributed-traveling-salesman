using System.Threading.Tasks;
using DistributedTravelingSalesman.Dto;

namespace DistributedTravelingSalesman.Service
{
    public interface IWorkerService
    {
        Task<GetOnlineWorkersResponseDto> GetOnlineWorkers();

        Task AddWorker(AddWorkerDto request);

        Task RemoveWorker(RemoveWorkerDto request);
    }
}