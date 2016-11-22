using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using AdaptPoint = AgGateway.ADAPT.ApplicationDataModel.Shapes.Point;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Boundaries.Models
{
    [TestFixture]
    public class PointTest
    {
        [Test]
        public void ShouldSetLongitude()
        {
            var adaptPoint = new AdaptPoint
            {
                X = 54.2341
            };

            var point = Point.FromAdaptPoint(adaptPoint);
            Assert.AreEqual(adaptPoint.X, point.Longitude);
        }

        [Test]
        public void ShouldSetLatitude()
        {
            var adaptPoint = new AdaptPoint
            {
                Y = 98.6546
            };

            var point = Point.FromAdaptPoint(adaptPoint);
            Assert.AreEqual(adaptPoint.Y, point.Latitude);
        }
    }
}
