using System.Collections.Generic;
using DistributedTravelingSalesman.Worker.Domain;
using Xunit;

namespace DistributedTravelingSalesman.Worker.Tests
{
    public class SwapperTests
    {
        [Fact]
        public void Swap_Should_SwapItems()
        {
            var list = new List<City>();
            var city1 = SampleFactory.CreateCity();
            var city2 = SampleFactory.CreateCity();
            list.Add(city1);
            list.Add(city2);
            
            Swapper.Swap(list, 0, 1);
            
            Assert.Equal("City B", list[0].Name);
            Assert.Equal("City A", list[1].Name);
        }
    }
}