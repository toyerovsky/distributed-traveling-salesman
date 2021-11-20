using System;
using System.Numerics;
using DistributedTravelingSalesman.Worker.Domain;

namespace DistributedTravelingSalesman.Worker.Tests
{
    public static class SampleFactory
    {
        private static int _charSequence = 'A';
        private static Random _random = new Random();
        
        public static City GetCity()
        {
            return new City
            {
                Name = $"City {_charSequence++}",
                Location = new Vector2(_random.NextSingle(), _random.NextSingle())
            };
        }
    }
}