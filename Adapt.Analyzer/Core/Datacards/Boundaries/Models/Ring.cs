using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;

namespace Adapt.Analyzer.Core.Datacards.Boundaries.Models
{
    public class Ring
    {
        public Point[] Points { get; }

        public Ring(Point[] points)
        {
            Points = points;
        }

        public static Ring FromLinearRing(LinearRing ring)
        {
            var points = ring.Points.Select(Point.FromAdaptPoint).ToArray();
            return new Ring(points);
        }
    }
}