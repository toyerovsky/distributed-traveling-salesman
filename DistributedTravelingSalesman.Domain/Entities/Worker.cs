using System;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public class Worker
    {
        public Guid Id { get; set; }
        public string Url { get; set; }

        public static Worker Create(string url)
        {
            return new Worker
            {
                Id = Guid.NewGuid(),
                Url = url
            };
        }
    }
}