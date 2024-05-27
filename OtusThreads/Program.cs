// See https://aka.ms/new-console-template for more information
using OtusThreads;
using System.Globalization;

WatchTest watch = new WatchTest(WatchTest.Iterations.Ten);

var numFmt = new CultureInfo("ru-RU", false).NumberFormat;
var itr = WatchTest.Iterations.Ten;
for (int i = 0; i < 4; i++)
{
    var intArray = ThreadResearch.GenerateRandom(100000 * (int)Math.Pow(10, i)); // 10^i * 100k
    //var x1 = ThreadResearch.SerialSum(intArray);
    //var x4 = ThreadResearch.ThreadSum(intArray);
    //var x2 = ThreadResearch.ParallelSum(intArray);
    //var x3 = ThreadResearch.ParallelLinqSum(intArray);
    //Console.WriteLine($"{x1} -- {x2} -- {x3}");
    WatchTest.WatchAction SerialSum = () => ThreadResearch.SerialSum(intArray);
    WatchTest.WatchAction ParallelSum = () => ThreadResearch.ParallelSum(intArray);
    WatchTest.WatchAction ParallelLinqSum = () => ThreadResearch.ParallelLinqSum(intArray);
    WatchTest.WatchAction ThreadSum = () => ThreadResearch.ThreadSum(intArray);


    Console.WriteLine($"---{intArray.Length.ToString("N0", numFmt)}---");
    Console.WriteLine($"Serial Sum time: \t{watch.Watch(SerialSum).WatchResults()}");
    Console.WriteLine($"Parallel Sum time: \t{watch.Watch(ParallelSum).WatchResults()}");
    Console.WriteLine($"PLinq Sum time: \t{watch.Watch(ParallelLinqSum).WatchResults()}");
    Console.WriteLine($"ThreadSum time: \t{watch.Watch(ThreadSum).WatchResults()}");
    Console.WriteLine();

}

// i5-8500T @2.1Ghz (6 cores), 16GB RAM
//---100 000-- -
//Serial Sum time:        Min: 0          Max: 0          Avg: 0          @10
//Parallel Sum time:      Min: 0          Max: 52         Avg: 5.3        @10
//PLinq Sum time:         Min: 0          Max: 16         Avg: 2.7        @10
//ThreadSum time:         Min: 34         Max: 64         Avg: 43.9       @10

//-- - 1 000 000-- -
//Serial Sum time:        Min: 2          Max: 2          Avg: 2          @10
//Parallel Sum time:      Min: 1          Max: 2          Avg: 1.1        @10
//PLinq Sum time:         Min: 5          Max: 50         Avg: 25.6       @10
//ThreadSum time:         Min: 33         Max: 50         Avg: 39.6       @10

//-- - 10 000 000-- -
//Serial Sum time:        Min: 23         Max: 30         Avg: 24.1       @10
//Parallel Sum time:      Min: 7          Max: 13         Avg: 10         @10
//PLinq Sum time:         Min: 19         Max: 164        Avg: 44         @10
//ThreadSum time:         Min: 59         Max: 109        Avg: 84.4       @10

//-- - 100 000 000-- -
//Serial Sum time:        Min: 239        Max: 337        Avg: 283.8      @10
//Parallel Sum time:      Min: 68         Max: 111        Avg: 90.2       @10
//PLinq Sum time:         Min: 163        Max: 205        Avg: 172.9      @10
//ThreadSum time:         Min: 398        Max: 525        Avg: 476        @10

