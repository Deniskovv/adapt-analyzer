using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Test.Adapt.Helpers;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;
using NUnit.Framework;
using FieldBoundary = AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries.FieldBoundary;
using Point = AgGateway.ADAPT.ApplicationDataModel.Shapes.Point;

namespace Adapt.Analyzer.Core.Test.Datacards.Boundaries
{
    [TestFixture]
    public class BoundaryReaderTest
    {
        private List<FieldBoundary> _boundaries;
        private BoundaryReader _boundaryReader;

        [SetUp]
        public void Setup()
        {
            _boundaries = new List<FieldBoundary>();

            _boundaryReader = new BoundaryReader();
        }

        [Test]
        public async Task ShouldGetOneExteriorRing()
        {
            AddBoundaryWithOneExteriorRing();

            var boundaries = await _boundaryReader.GetBoundaries(_boundaries);
            AssertOneExteriorRing(boundaries);
        }

        [Test]
        public async Task ShouldGetMultipleExteriorRings()
        {
            AddBoundaryWithMultipleExteriorRings();

            var boundaries = await _boundaryReader.GetBoundaries(_boundaries);
            AssertMultipleExteriorRings(boundaries);
        }

        [Test]
        public async Task ShouldGetOneInteriorRing()
        {
            AddBoundaryWithOneInteriorRing();

            var boundaries = await _boundaryReader.GetBoundaries(_boundaries);
            AssertOneInteriorRing(boundaries);
        }

        [Test]
        public async Task ShouldGetMultipleInteriorRings()
        {
            AddBoundaryWithMultipleInteriorRings();

            var boundaries = await _boundaryReader.GetBoundaries(_boundaries);
            AssertMultipleInteriorRings(boundaries);
        }

        [Test]
        public async Task ShouldGetMultipleBoundaries()
        {
            AddBoundaryWithOneExteriorRing();
            AddBoundaryWithMultipleExteriorRings();
            AddBoundaryWithMultipleInteriorRings();
            AddBoundaryWithOneInteriorRing();

            var boundaries = await _boundaryReader.GetBoundaries(_boundaries);
            Assert.AreEqual(4, boundaries.Length);
        }

        private void AddBoundaryWithOneExteriorRing()
        {
            _boundaries.Add(new FieldBoundary
            {
                SpatialData = new MultiPolygon
                {
                    Polygons = new List<Polygon>
                    {
                        new Polygon
                        {
                            ExteriorRing = CreateSimplestRing()
                        }
                    }
                }
            });
        }

        private void AddBoundaryWithMultipleExteriorRings()
        {
            _boundaries.Add(new FieldBoundary
            {
                SpatialData = new MultiPolygon
                {
                    Polygons = new List<Polygon>
                    {
                        new Polygon
                        {
                            ExteriorRing = CreateSimplestRing()
                        },
                        new Polygon
                        {
                            ExteriorRing = CreateSimplestRing()
                        },
                        new Polygon
                        {
                            ExteriorRing = CreateSimplestRing()
                        }
                    }
                }
            });
        }

        private void AddBoundaryWithOneInteriorRing()
        {
            _boundaries.Add(new FieldBoundary
            {
                SpatialData = new MultiPolygon
                {
                    Polygons = new List<Polygon>
                    {
                        new Polygon
                        {
                            ExteriorRing = CreateSimplestRing(),
                            InteriorRings = new List<LinearRing>
                            {
                                CreateSimplestRing()
                            }
                        }
                    }
                }
            });
        }

        private void AddBoundaryWithMultipleInteriorRings()
        {
            _boundaries.Add(new FieldBoundary
            {
                SpatialData = new MultiPolygon
                {
                    Polygons = new List<Polygon>
                    {
                        new Polygon
                        {
                            ExteriorRing = CreateSimplestRing(),
                            InteriorRings = new List<LinearRing>
                            {
                                CreateSimplestRing(),
                                CreateSimplestRing(),
                                CreateSimplestRing()
                            }
                        }
                    }
                }
            });
        }

        private static void AssertOneExteriorRing(Boundary[] boundaries)
        {
            Assert.AreEqual(1, boundaries.Length);
            Assert.AreEqual(1, boundaries[0].Exteriors.Length);
            Assert.AreEqual(5, boundaries[0].Exteriors[0].Points.Length);
        }

        private static void AssertMultipleExteriorRings(Boundary[] boundaries)
        {
            Assert.AreEqual(1, boundaries.Length);
            var boundary = boundaries[0];
            Assert.AreEqual(3, boundary.Exteriors.Length);
            Assert.AreEqual(5, boundary.Exteriors[0].Points.Length);
            Assert.AreEqual(5, boundary.Exteriors[1].Points.Length);
            Assert.AreEqual(5, boundary.Exteriors[2].Points.Length);
        }

        private static void AssertOneInteriorRing(Boundary[] boundaries)
        {
            Assert.AreEqual(1, boundaries.Length);
            Assert.AreEqual(1, boundaries[0].Interiors.Length);
            Assert.AreEqual(5, boundaries[0].Interiors[0].Points.Length);
        }

        private static void AssertMultipleInteriorRings(Boundary[] boundaries)
        {
            Assert.AreEqual(1, boundaries.Length);
            var boundary = boundaries[0];
            Assert.AreEqual(3, boundary.Interiors.Length);
            Assert.AreEqual(5, boundary.Interiors[0].Points.Length);
            Assert.AreEqual(5, boundary.Interiors[1].Points.Length);
            Assert.AreEqual(5, boundary.Interiors[2].Points.Length);
        }

        private static LinearRing CreateSimplestRing()
        {
            return new LinearRing
            {
                Points = new List<Point>
                {
                    PointFactory.CreatePoint(0, 0), PointFactory.CreatePoint(0, 1), PointFactory.CreatePoint(1, 1), PointFactory.CreatePoint(1, 0), PointFactory.CreatePoint(0, 0),
                }
            };
        }
    }
}
