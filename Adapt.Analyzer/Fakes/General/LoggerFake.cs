using System;
using System.Collections.Generic;
using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class LoggerFake : ILogger
    {
        public List<string> InfoMessages { get; }
        public List<string> ErrorMessages { get; }
        public List<Exception> Exceptions { get; }

        public LoggerFake()
        {
            Exceptions = new List<Exception>();
            ErrorMessages = new List<string>();
            InfoMessages = new List<string>();
        }
        

        public void InfoFormat(string message, params object[] args)
        {
            InfoMessages.Add(string.Format(message, args));
        }

        public void ExceptionFormat(string message, Exception exception, object[] args)
        {
            ErrorMessages.Add(string.Format(message, args));
            Exceptions.Add(exception);
        }
    }
}
