using System;
using log4net;

namespace Adapt.Analyzer.Core.General
{
    public interface ILogger
    {
        void InfoFormat(string message, params object[] args);
        void ExceptionFormat(string message, Exception exception, params object[] args);
    }

    public class Logger : ILogger
    {
        private readonly ILog _log;

        public Logger()
            : this(LogManager.GetLogger("Adapt.Analyzer"))
        {
            
        }

        public Logger(ILog log)
        {
            _log = log;
        }

        public void InfoFormat(string message, params object[] args)
        {
            _log.InfoFormat(message, args);
        }

        public void ExceptionFormat(string message, Exception exception, params object[] args)
        {
            _log.ErrorFormat(message, args);
            _log.Error(exception);
        }
    }
}