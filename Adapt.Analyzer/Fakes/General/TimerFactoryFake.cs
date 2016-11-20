using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class TimerFactoryFake : ITimerFactory
    {
        private readonly TimerFake _timer;

        public TimerFactoryFake(TimerFake timer)
        {
            _timer = timer;
        }

        public ITimer Create()
        {
            return _timer ?? new TimerFake();
        }
    }
}
