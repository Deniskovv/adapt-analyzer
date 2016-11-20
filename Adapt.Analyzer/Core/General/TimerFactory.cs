namespace Adapt.Analyzer.Core.General
{
    public interface ITimerFactory
    {
        ITimer Create();
    }

    public class TimerFactory : ITimerFactory
    {
        public ITimer Create()
        {
            return new Timer();
        }
    }
}
