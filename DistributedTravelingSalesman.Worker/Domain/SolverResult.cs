using System;
using System.Collections.Generic;

namespace DistributedTravelingSalesman.Worker.Domain
{
    public class SolverResult
    {
        public List<City> Cities { get; set; }
        public TimeSpan Time { get; set; }
        public int PermutationCount { get; set; }
    }
}