using System;
using System.Collections.Generic;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public class SolverResult
    {
        public List<int> Nodes { get; set; }
        public double Route { get; set; }
        public TimeSpan Time { get; set; }
    }
}