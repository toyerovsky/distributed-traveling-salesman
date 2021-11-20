using System;
using System.Numerics;
using DistributedTravelingSalesman.Worker.Domain;
using Xunit;

namespace DistributedTravelingSalesman.Worker.Tests
{
    public class CityTests
    {
        [Fact]
        public void GetDistanceTo_ShouldReturnZero_WhenLocationIsTheSame()
        {
            var city1 = SampleFactory.GetCity();
            city1.Location = new Vector2(1f, 1f);
            var city2 = SampleFactory.GetCity();
            city2.Location = new Vector2(1f, 1f);

            Assert.Equal(0f, city1.GetDistanceTo(city2));
        }
        
        [Fact]
        public void GetDistanceTo_ShouldReturnSqrtOfTwo_WhenCity1Is_0_0_And_City2Is_1_1()
        {
            var city1 = SampleFactory.GetCity();
            city1.Location = new Vector2(0f, 0f);
            var city2 = SampleFactory.GetCity();
            city2.Location = new Vector2(1f, 1f);

            Assert.Equal(Math.Sqrt(2), city1.GetDistanceTo(city2));
        }
    }
}