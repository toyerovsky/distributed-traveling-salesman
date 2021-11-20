using System;
using System.Numerics;

namespace DistributedTravelingSalesman.Worker.Domain
{
    public class City
    {
        public string Name { get; set; }
        public Vector2 Location { get; set; }

        public double GetDistanceTo(City city)
        {
            return Math.Sqrt(Math.Pow(this.Location.X - city.Location.X, 2) + Math.Pow(this.Location.Y - city.Location.Y, 2));
        }
    }
}