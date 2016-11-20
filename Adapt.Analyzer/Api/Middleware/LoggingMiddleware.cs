using System;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.General;
using Microsoft.Owin;

namespace Adapt.Analyzer.Api.Middleware
{
    public class LoggingMiddleware : OwinMiddleware
    {
        private readonly ILogger _logger;
        private readonly ITimerFactory _timerFactory;

        public LoggingMiddleware(OwinMiddleware next)
            : this(next, new Logger(), new TimerFactory())
        {

        }

        public LoggingMiddleware(OwinMiddleware next, ILogger logger, ITimerFactory timerFactory) : base(next)
        {
            _logger = logger;
            _timerFactory = timerFactory;
        }

        public override async Task Invoke(IOwinContext context)
        {
            _logger.InfoFormat("Starting Request for: {0}", context.Request.Uri);
            try
            {
                await TryNext(context);
            }
            catch (Exception ex)
            {
                _logger.ExceptionFormat("Request for {0} failed.", ex, context.Request.Uri);
                throw;
            }
            
        }

        private async Task TryNext(IOwinContext context)
        {
            var timer = _timerFactory.Create();
            timer.Start();
            await Next.Invoke(context);
            timer.Stop();
            _logger.InfoFormat("Request for {0} took: {1} ms", context.Request.Uri, timer.Elapsed.TotalMilliseconds);
        }
    }
}