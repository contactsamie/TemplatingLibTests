using System;

namespace TemplatingLibTests.BenchMarkUtility
{
    public class Clock
    {
        public static void Benchmark(string description, Action action, int iterations = 10000)
        {
            Console.WriteLine("Time benchmark -----------------");
            BenchmarkTime(description, action, iterations);
            Console.WriteLine("Cpu benchmark ------------------");
            BenchmarkCpu(description, action, iterations);
        }


        public static void BenchmarkTime(string description, Action action, int iterations = 10000)
        {
            Benchmark<TimeWatch>(description, action, iterations);
        }

        private static void Benchmark<T>(string description, Action action, int iterations) where T : IStopwatch, new()
        {
            Console.WriteLine("BenchMarking : " + description);
            //clean Garbage
            GC.Collect();

            //wait for the finalizer queue to empty
            GC.WaitForPendingFinalizers();

            //clean Garbage
            GC.Collect();

            //warm up
            action();

            var stopwatch = new T();
            var timings = new double[5];
            for (var i = 0; i < timings.Length; i++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (var j = 0; j < iterations; j++)
                    action();
                stopwatch.Stop();
                timings[i] = stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine(timings[i] + " (ms)");
            }
            Console.WriteLine("normalized mean: " + timings.NormalizedMean() + " (ms)");
            Console.WriteLine("BenchMarking : " + description + " Completed **********");
        }

        public static void BenchmarkCpu(string description, Action action, int iterations = 10000)
        {
            Benchmark<CpuWatch>(description, action, iterations);
        }
    }
}