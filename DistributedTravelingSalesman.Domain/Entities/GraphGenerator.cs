using System;
using System.Collections.Generic;
using System.Numerics;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public class GraphGenerator
    {
        private const int MinX = 0;
        private const int MaxX = 1000;
        private const int MinY = 0;
        private const int MaxY = 1000;
        private readonly Random _random;

        public GraphGenerator()
        {
            _random = new Random();
        }

        public Graph GenerateGraph(int nodesCount)
        {
            var consistentGraph = GenerateRandomGraph(nodesCount);
            var graphDto = new Graph
            {
                AdjMatrix = ConvertToMatrix(consistentGraph)
            };
            return graphDto;
        }

        public IList<Node> GenerateRandomGraph(int nodesCount)
        {
            var graph = new List<Node>();
            for (var i = 0; i < nodesCount; i++)
            {
                var x = _random.Next(MinX, MaxX);
                var y = _random.Next(MinY, MaxY);
                graph.Add(new Node(i, new Vector2(x, y)));
            }

            foreach (var outerNode in graph)
            foreach (var innerNode in graph)
                if (innerNode != outerNode)
                    outerNode.AddEdge(new Edge(outerNode, innerNode,
                        Math.Round(outerNode.GetDistanceTo(innerNode), 2)));

            return graph;
        }

        public double[][] ConvertToMatrix(IList<Node> graph)
        {
            var graphMatrix = GetTwoDimensionalArray<double>(graph.Count);

            foreach (var node in graph)
            foreach (var edge in node.Edges)
            {
                var secondIndex = edge.FirstNode == node ? edge.SecondNode.Index : edge.FirstNode.Index;
                graphMatrix[node.Index][secondIndex] = edge.Weight;
            }

            return graphMatrix;
        }

        public T[][] GetTwoDimensionalArray<T>(int size)
        {
            var array = new T[size][];
            for (var i = 0; i < size; i++)
            {
                var innerArray = new T[size];
                array[i] = innerArray;
            }

            return array;
        }
    }
}