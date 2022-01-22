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

        private static bool NextPermutation(List<int> list)
        {
            if (list.Count < 2) return false;
            var k = list.Count - 2;

            while (k >= 0 && list[k].CompareTo(list[k + 1]) >= 0) k--;
            if (k < 0) return false;

            var l = list.Count - 1;
            while (l > k && list[l].CompareTo(list[k]) <= 0) l--;

            list[k] ^= list[l];
            list[l] ^= list[k];
            list[k] ^= list[l];

            var i = k + 1;
            var j = list.Count - 1;
            while (i < j)
            {
                list[i] ^= list[j];
                list[j] ^= list[i];
                list[i] ^= list[j];
                i++;
                j--;
            }

            return true;
        }
    }
}