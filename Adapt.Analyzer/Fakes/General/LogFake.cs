using System;
using System.Collections.Generic;
using log4net;
using log4net.Core;

namespace Fakes.General
{
    public class LogFake : ILog
    {
        public ILogger Logger { get; set; }
        public bool IsDebugEnabled { get; }
        public bool IsInfoEnabled { get; }
        public bool IsWarnEnabled { get; }
        public bool IsErrorEnabled { get; }
        public bool IsFatalEnabled { get; }
        public List<string> InfoMessages { get; }
        public List<string> ErrorMessages { get; }
        public List<Exception> Exceptions { get; }


        public LogFake()
        {
            InfoMessages = new List<string>();
            ErrorMessages = new List<string>();
            Exceptions = new List<Exception>();
        }

        public void Debug(object message)
        {
            throw new NotImplementedException();
        }

        public void Debug(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(object message)
        {
            throw new NotImplementedException();
        }

        public void Info(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, params object[] args)
        {
            InfoMessages.Add(string.Format(format, args));
        }

        public void InfoFormat(string format, object arg0)
        {
            InfoFormat(format, new[] { arg0 });
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            InfoFormat(format, new[] { arg0, arg1 });
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            InfoFormat(format, new[] { arg0, arg1, arg2 });
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(object message)
        {
            Exceptions.Add(message as Exception);
        }

        public void Error(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(string format, params object[] args)
        {
            ErrorMessages.Add(string.Format(format, args));
        }

        public void ErrorFormat(string format, object arg0)
        {
            ErrorFormat(format, new [] { arg0 });
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            ErrorFormat(format, new[] { arg0, arg1 });
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            ErrorFormat(format, new[] { arg0, arg1, arg2 });
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
