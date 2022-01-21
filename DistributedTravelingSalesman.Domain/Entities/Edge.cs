namespace DistributedTravelingSalesman.Domain.Entities
{
    public class Edge
    {
        public Edge(Node firstNode, Node secondNode, double weight)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            Weight = weight;
        }

        public Node FirstNode { get; init; }
        public Node SecondNode { get; init; }
        public double Weight { get; init; }
    }
}