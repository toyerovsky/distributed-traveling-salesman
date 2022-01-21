using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DistributedTravelingSalesman.Domain.Entities;
using DistributedTravelingSalesman.Dto;

namespace DistributedTravelingSalesman.Service
{
    public interface IGraphService
    {
        Stream GenerateGraph(int numberOfNodes);

        Task<GetBestHamiltonianPathResponseDto> GetBestHamiltonianPath(IList<Worker> workers,
            GetBestHamiltonianPathRequestDto request);
    }
}