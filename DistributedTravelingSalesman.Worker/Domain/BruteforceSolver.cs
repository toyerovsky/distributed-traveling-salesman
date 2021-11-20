using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DistributedTravelingSalesman.Worker.Domain
{
    public class BruteforceSolver
    {
        private List<City> _currentMinimum;
        private double minLength = double.MaxValue;

        public SolverResult Solve(List<City> cities, int startIndex)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            FindUsingBruteforce(cities, startIndex, 0);
            stopwatch.Stop();
            
            return new SolverResult
            {
                Cities = _currentMinimum,
                PermutationCount = MathExtensions.Factorial(cities.Count),
                Time = stopwatch.Elapsed
            };
        }
        
        private void FindUsingBruteforce(List<City> cities, int startIndex, int i)
        {
            if (_currentMinimum.Count == 0) {
                Swapper.Swap(cities, 0, startIndex);
            }

            if (i == cities.Count) {
                double length = 0d;
                for (int j = 0; j < cities.Count - 1; ++j)
                    length += cities[j].GetDistanceTo(cities[j + 1]);

                if (length < minLength) {
                    _currentMinimum = cities;
                    minLength = length;
                }
                return;
            }

            for (int j = i; j < cities.Count; ++j) {
                Swapper.Swap(cities, i, j);
                FindUsingBruteforce(cities, startIndex, i + 1);
                Swapper.Swap(cities, i, j);
            }
        }
        
    }
}