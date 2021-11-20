namespace DistributedTravelingSalesman.Worker.Domain
{
    public static class MathExtensions
    {
        public static int Factorial(int f)
        {
            if (f == 0)
                return 1;
            else
                return f * Factorial(f - 1);
        }
    }
}