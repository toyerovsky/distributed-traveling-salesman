using System;
using System.Collections.Generic;
using System.Numerics;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public class Node
    {
        public Node(int index, Vector2 location)
        {
            Index = index;
            Location = location;
            Edges = new List<Edge>();
        }

        public Vector2 Location { get; }

        public int Index { get; }

        public List<Edge> Edges { get; }

        public double GetDistanceTo(Node node)
        {
            return Math.Sqrt(Math.Pow(Location.X - node.Location.X, 2) + Math.Pow(Location.Y - node.Location.Y, 2));
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }
    }
}