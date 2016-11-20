using System;
using Adapt.Analyzer.Core.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.General
{
    [TestFixture]
    public class TimerTest
    {
        private TimerFactory _timerFactory;

        [SetUp]
        public void Setup()
        {
            _timerFactory = new TimerFactory();
        }

        [Test]
        public void StartShouldStartTimer()
        {
            var timer = _timerFactory.Create();
            timer.Start();
            Assert.Greater(timer.Elapsed, TimeSpan.FromMilliseconds(0));
        }

        [Test]
        public void StopShouldStopTimer()
        {
            var timer = _timerFactory.Create();
            timer.Start();
            timer.Stop();
            Assert.Greater(timer.Elapsed, TimeSpan.FromMilliseconds(0));
            Assert.Less(timer.Elapsed, TimeSpan.FromMilliseconds(2));
        }
    }
}
