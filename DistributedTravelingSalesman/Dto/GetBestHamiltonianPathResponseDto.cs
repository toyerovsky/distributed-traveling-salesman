using System.Collections.Generic;

namespace DistributedTravelingSalesman.Dto
{
    public class GetBestHamiltonianPathResponseDto
    {
        public List<int> Indexes { get; set; }
        public double TotalRoute { get; set; }
    }
}