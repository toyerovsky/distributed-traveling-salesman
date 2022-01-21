using DistributedTravelingSalesman.Domain.Entities;
using Xunit;

namespace DistributedTravelingSalesman.Worker.Tests
{
    public class MathExtensionsTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        public void Factorial_Should_ReturnProperFactorial(int f, int result)
        {
            Assert.Equal(result, MathExtensions.Factorial(f));
        }
    }
}