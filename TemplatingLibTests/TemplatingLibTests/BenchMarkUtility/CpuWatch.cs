using System;
using System.Diagnostics;

namespace TemplatingLibTests.BenchMarkUtility
{
    public class CpuWatch : IStopwatch
    {
        private TimeSpan _endTime;
        private TimeSpan _startTime;


        public TimeSpan Elapsed
        {
            get
            {
                if (IsRunning)
                    throw new NotImplementedException("Getting elapsed span while watch is running is not implemented");

                return _endTime - _startTime;
            }
        }

        public bool IsRunning { get; private set; }


        public void Start()
        {
            _startTime = Process.GetCurrentProcess().TotalProcessorTime;
            IsRunning = true;
        }

        public void Stop()
        {
            _endTime = Process.GetCurrentProcess().TotalProcessorTime;
            IsRunning = false;
        }

        public void Reset()
        {
            _startTime = TimeSpan.Zero;
            _endTime = TimeSpan.Zero;
        }
    }
}