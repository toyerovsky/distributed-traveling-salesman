using System;
using System.Collections.Generic;
using System.Linq;
using DistributedTravelingSalesman.Domain.Entities;

namespace DistributedTravelingSalesman.Dto
{
    public class GetOnlineWorkersResponseDto
    {
        public IEnumerable<ListItem> Data { get; set; }

        public IList<Worker> GetWorkerList()
        {
            return Data.Select(x => new Worker
            {
                Id = x.Id,
                Url = x.Url
            }).ToList();
        }

        public class ListItem
        {
            public Guid Id { get; set; }
            public string Url { get; set; }
        }
    }
}