using System.Collections.Generic;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public static class Swapper
    {
        public static void Swap<T>(IList<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }
    }
}