using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OtusThreads
{
    public class ThreadResearch
    {
        public static int[] GenerateRandom(int randomCount)
        {
            var rnd = new Random();
            var result = new List<int> ();
            for (int x = 0; x < randomCount; x++)
            {
                result.Add(rnd.Next(randomCount));
            }
            return result.ToArray();
        }

        public static long SerialSum(int[] intArr)
        {
            long result = 0;
            foreach (var x in intArr)
            {
                result += x;
            }
            return result;
        }

        public static long ParallelSum(int[] intArr)
        {
            long result = 0;
            var parts = Partitioner.Create(0, intArr.Length).AsParallel();
            var opts = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.ForEach(parts, opts, part =>
            {
                long tmpSum = 0;


                for (int i = part.Item1; i < part.Item2; i++)
                {
                    tmpSum += intArr[i];
                }
                Interlocked.Add(ref result, tmpSum);
            });
            return result;
        }

        public static long ParallelLinqSum(int[] intArr)
        {
            long result = intArr.AsParallel().Sum(s => (long)s);
            return result;
        }

        public static long ThreadSum(int[] intArr)
        {
            long result = 0;
            int numThreads = Environment.ProcessorCount;
            int chunkSize = intArr.Length / numThreads;
            
            var parts = Partitioner.Create(0, intArr.Length, chunkSize).AsParallel().ToArray();
            var threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                threads[i] = new Thread(() =>
                {
                    long tmpSum = 0;
                    for (int j = parts[i].Item1; j < parts[i].Item2; j++)
                    {
                        tmpSum += intArr[j];
                    }
                    Interlocked.Add(ref result, tmpSum);
                });
                threads[i].Start();
            }
            

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            return result;
        }
    }
}
