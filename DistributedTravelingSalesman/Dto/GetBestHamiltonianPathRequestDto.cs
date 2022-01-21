using DistributedTravelingSalesman.Domain.Entities;

namespace DistributedTravelingSalesman.Dto
{
    public class GetBestHamiltonianPathRequestDto
    {
        public int StartIndex { get; set; }
        public Graph Graph { get; set; }
    }
}