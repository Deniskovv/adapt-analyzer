using System;
using Adapt.Analyzer.Core.General;
using log4net;
using Moq;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.General
{
    [TestFixture]
    public class LoggerTest
    {
        private Mock<ILog> _logMock;
        private Logger _logger;

        [SetUp]
        public void Setup()
        {
            _logMock = new Mock<ILog>();
            _logger = new Logger(_logMock.Object);
        }

        [Test]
        public void ShouldLogInfo()
        {
            _logger.InfoFormat("This is info");
            _logMock.Verify(s => s.InfoFormat("This is info"), Times.Once());
        }

        [Test]
        public void ShouldLogFormattedInfo()
        {
            _logger.InfoFormat("Something {0} goes {1} here {2}", "one", "two", "three");
            _logMock.Verify(s => s.InfoFormat("Something {0} goes {1} here {2}", new object[] {"one", "two", "three"}), Times.Once());
        }

        [Test]
        public void ShouldLogException()
        {
            var exception = new Exception();
            _logger.ExceptionFormat("I have {0} errors {1} {2}", exception, "three", "one", "two");
            _logMock.Verify(s => s.ErrorFormat("I have {0} errors {1} {2}", new object[] {"three", "one", "two"}), Times.Once());
            _logMock.Verify(s => s.Error(exception), Times.Once());
        }
    }
}
