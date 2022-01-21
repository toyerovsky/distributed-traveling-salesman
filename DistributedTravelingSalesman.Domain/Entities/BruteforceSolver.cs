using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DistributedTravelingSalesman.Domain.Entities
{
    public class BruteforceSolver
    {
        private List<int> _currentMinimum = new();
        private double _currentMinimumRoute = double.MaxValue;
        private List<int> _currentStep = new();

        public void Clear()
        {
            _currentMinimum.Clear();
            _currentMinimumRoute = double.MaxValue;
            _currentStep.Clear();
        }


        public SolverResult Solve(Graph graph, int startIndex, int firstStep)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _currentStep = Enumerable.Range(0, graph.GraphSize).ToList();

            do
            {
                if (_currentStep[0] == startIndex && _currentStep[1] == firstStep)
                {
                    double route = 0;
                    for (var i = 0; i < _currentStep.Count - 1; i++)
                        route += graph.AdjMatrix[_currentStep[i]][_currentStep[i + 1]];
                    if (_currentMinimumRoute > route)
                    {
                        _currentMinimumRoute = route;
                        _currentMinimum = new List<int>(_currentStep);
                    }
                }
            } while (NextPermutation(_currentStep));

            stopwatch.Stop();

            return new SolverResult
            {
                Nodes = new List<int>(_currentMinimum),
                Route = _currentMinimumRoute,
                Time = stopwatch.Elapsed
            };
        }

        private static bool NextPermutation<T>(IList<T> a) where T : IComparable
        {
            if (a.Count < 2) return false;
            var k = a.Count - 2;

            while (k >= 0 && a[k].CompareTo(a[k + 1]) >= 0) k--;
            if (k < 0) return false;

            var l = a.Count - 1;
            while (l > k && a[l].CompareTo(a[k]) <= 0) l--;


            Swapper.Swap(a, k, l);

            var i = k + 1;
            var j = a.Count - 1;
            while (i < j)
            {
                Swapper.Swap(a, i, j);
                i++;
                j--;
            }

            return true;
        }
    }
}