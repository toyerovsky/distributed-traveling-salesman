using System.Collections.Generic;

namespace DistributedTravelingSalesman.Dto
{
    public class FindBestPartialResultRequestDto
    {
        public int StartIndex { get; set; }
        public IList<int> Chunks { get; set; }
    }
}