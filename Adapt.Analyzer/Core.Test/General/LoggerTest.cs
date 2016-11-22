using System;
using Adapt.Analyzer.Core.General;
using Fakes.General;
using log4net;
using Moq;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.General
{
    [TestFixture]
    public class LoggerTest
    {
        private LogFake _logFake;
        private Logger _logger;

        [SetUp]
        public void Setup()
        {
            _logFake = new LogFake();
            _logger = new Logger(_logFake);
        }

        [Test]
        public void ShouldLogInfo()
        {
            _logger.InfoFormat("This is info");
            Assert.Contains("This is info", _logFake.InfoMessages);
        }

        [Test]
        public void ShouldLogFormattedInfo()
        {
            _logger.InfoFormat("Something {0} goes {1} here {2}", "one", "two", "three");
            Assert.Contains("Something one goes two here three", _logFake.InfoMessages);
        }

        [Test]
        public void ShouldLogException()
        {
            var exception = new Exception();
            _logger.ExceptionFormat("I have {0} errors {1} {2}", exception, "three", "one", "two");
            Assert.Contains("I have three errors one two", _logFake.ErrorMessages);
            Assert.Contains(exception, _logFake.Exceptions);
        }
    }
}
