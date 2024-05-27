using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtusThreads
{
    public class WatchTest
    {
        Iterations _itr;
        public WatchTest(Iterations itr = Iterations.One) 
        { 
            _itr = itr;
        }

        public enum Iterations { One = 1, Five = 5, Ten = 10 }
        public delegate object WatchAction();
        public List<long> Watch(WatchAction action)
        {
            var result = new List<long>();
            for (int i = 0; i < (int)_itr; i++)
            {
                var stopwatch = Stopwatch.StartNew();
                action();
                stopwatch.Stop();
                result.Add(stopwatch.ElapsedMilliseconds);
            }
            return result;
            
        }
    }

    public static class WatchTestHelper
    {
        public static string WatchResults(this List<long> results)
        {
            return $"Min: {results.Min()}  \tMax: {results.Max()}  \tAvg: {results.Average()}  \t@{results.Count()}";
        }
    }
}
