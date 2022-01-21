using System;
using System.Numerics;
using DistributedTravelingSalesman.Domain.Entities;

namespace DistributedTravelingSalesman.Worker.Tests
{
    public static class SampleFactory
    {
        private static int _charSequence = 'A';
        private static readonly Random _random = new();

        public static Node CreateCity()
        {
            return new Node
            {
                Location = new Vector2(_random.NextSingle(), _random.NextSingle())
            };
        }
    }
}