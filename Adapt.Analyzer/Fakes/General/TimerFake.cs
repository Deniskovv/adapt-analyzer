using System;
using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class TimerFake : ITimer
    {
        public bool IsStarted { get; private set; }
        public TimeSpan Elapsed { get; set; }
        public bool IsStopped { get; private set; }

        public void Start()
        {
            IsStarted = true;
        }

        public void Stop()
        {
            IsStopped = true;
        }
    }
}
