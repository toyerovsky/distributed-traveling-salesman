namespace DistributedTravelingSalesman.Domain.Entities
{
    public static class MathExtensions
    {
        public static int Factorial(int f)
        {
            if (f == 0)
                return 1;
            return f * Factorial(f - 1);
        }
    }
}