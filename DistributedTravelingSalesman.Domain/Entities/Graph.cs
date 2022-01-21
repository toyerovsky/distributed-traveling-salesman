using System.Text.Json.Serialization;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public class Graph
    {
        public double[][] AdjMatrix { get; set; }

        public double this[int x, int y] => AdjMatrix[x][y];

        [JsonIgnore] public int GraphSize => AdjMatrix?.Length ?? 0;
    }
}