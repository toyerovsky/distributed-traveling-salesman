using System.Collections.Generic;

namespace DistributedTravelingSalesman.Dto
{
    public class FindBestPartialResultResponseDto
    {
        public List<int> Nodes { get; set; }
        public double Route { get; set; }
    }
}