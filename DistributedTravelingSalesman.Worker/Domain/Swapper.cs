using System.Collections.Generic;

namespace DistributedTravelingSalesman.Worker.Domain
{
    public static class Swapper
    {
        public static void Swap<T>(List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }
    }
}