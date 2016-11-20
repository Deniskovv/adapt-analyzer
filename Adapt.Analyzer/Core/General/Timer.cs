using System;
using System.Diagnostics;

namespace Adapt.Analyzer.Core.General
{
    public interface ITimer
    {
        TimeSpan Elapsed { get; }
        void Start();
        void Stop();
    }

    public class Timer : ITimer
    {
        private readonly Stopwatch _stopwatch;

        public TimeSpan Elapsed => _stopwatch.Elapsed;

        public Timer()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
    }
}
