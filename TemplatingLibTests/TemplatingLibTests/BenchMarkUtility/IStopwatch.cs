using System;

namespace TemplatingLibTests.BenchMarkUtility
{
    public interface IStopwatch
    {
        bool IsRunning { get; }
        TimeSpan Elapsed { get; }

        void Start();
        void Stop();
        void Reset();
    }
}