using System.Collections.Generic;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Test.Adapt.Helpers;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;
using NUnit.Framework;
using Point = AgGateway.ADAPT.ApplicationDataModel.Shapes.Point;

namespace Adapt.Analyzer.Core.Test.Datacards.Boundaries.Models
{
    [TestFixture]
    public class RingTest
    {
        [Test]
        public void ShouldGetAllPoints()
        {
            var linearRing = new LinearRing
            {
                Points = new List<Point>
                {
                    PointFactory.CreatePoint(0, 0),
                    PointFactory.CreatePoint(.5, 1),
                    PointFactory.CreatePoint(1, 0),
                    PointFactory.CreatePoint(0, 0)
                }
            };

            var ring = Ring.FromLinearRing(linearRing);
            Assert.AreEqual(4, ring.Points.Length);
        }
    }
}
