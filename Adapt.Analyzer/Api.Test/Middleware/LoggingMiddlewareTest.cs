using System;
using System.Threading.Tasks;
using Adapt.Analyzer.Api.Middleware;
using Fakes.General;
using Fakes.Owin;
using Fakes.Owin.Middleware;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Middleware
{
    [TestFixture]
    public class LoggingMiddlewareTest
    {
        private TimerFake _timerFake;
        private TimerFactoryFake _timerFactoryFake;
        private LoggerFake _loggerFake;
        private OwinMiddlewareFake _nextFake;
        private LoggingMiddleware _loggingMiddleware;
        private OwinContextFake _context;

        [SetUp]
        public void Setup()
        {
            _timerFake = new TimerFake();
            _timerFactoryFake = new TimerFactoryFake(_timerFake);
            _loggerFake = new LoggerFake();
            _context = new OwinContextFake();
            _nextFake = new OwinMiddlewareFake();
            _loggingMiddleware = new LoggingMiddleware(_nextFake, _loggerFake, _timerFactoryFake);
        }

        [Test]
        public async Task ShouldLogStartOfRequest()
        {
            SetOwinRequest("http://something.com/else/stuff");

            await _loggingMiddleware.Invoke(_context);
            Assert.Contains("Starting Request for: http://something.com/else/stuff", _loggerFake.InfoMessages);
        }

        [Test]
        public async Task ShouldLogTimeOfRequest()
        {
            _timerFake.Elapsed = TimeSpan.FromMilliseconds(34);
            SetOwinRequest("http://google.com");

            await _loggingMiddleware.Invoke(_context);
            Assert.Contains("Request for http://google.com/ took: 34 ms", _loggerFake.InfoMessages);
        }

        [Test]
        public async Task ShouldStartTimerForRequest()
        {
            SetOwinRequest("http://google.com");

            await _loggingMiddleware.Invoke(_context);
            Assert.IsTrue(_timerFake.IsStarted);
        }

        [Test]
        public async Task ShouldStopTimerForRequest()
        {
            SetOwinRequest("http://google.com");

            await _loggingMiddleware.Invoke(_context);
            Assert.IsTrue(_timerFake.IsStopped);
        }

        [Test]
        public async Task ShouldLogExceptionForRequest()
        {
            _nextFake.Exception = new Exception();
            SetOwinRequest("http://bob.com");

            Assert.ThrowsAsync<Exception>(() => _loggingMiddleware.Invoke(_context));
            Assert.Contains("Request for http://bob.com/ failed.", _loggerFake.ErrorMessages);
            Assert.Contains(_nextFake.Exception, _loggerFake.Exceptions);
        }

        private void SetOwinRequest(string httpGoogleCom)
        {
            _context.Request = new OwinRequestFake
            {
                Uri = new Uri(httpGoogleCom)
            };
        }
    }
}
