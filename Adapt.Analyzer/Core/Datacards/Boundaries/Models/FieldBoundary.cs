using System.Collections.Generic;
using System.Linq;

namespace Adapt.Analyzer.Core.Datacards.Boundaries.Models
{
    public class FieldBoundary
    {
        public Boundary[] Boundaries { get; }
        public string Description { get; }
        public Point CenterPoint { get; }
        public int Id { get; }

        public FieldBoundary(int id, string description, Boundary[] boundaries)
        {
            Boundaries = boundaries;
            Id = id;
            CenterPoint = GetCenterPoint(boundaries);
            Description = description;
        }

        private static Point GetCenterPoint(Boundary[] boundaries)
        {
            if (boundaries == null)
                return null;

            var points = boundaries.SelectMany(b => b.Exteriors).SelectMany(e => e.Points).ToArray();
            var centerLongitude = GetCenterLongitude(points);

            var centerLatitude = GetCenterLatitude(points);
            return new Point(centerLongitude, centerLatitude);
        }

        private static double GetCenterLatitude(IEnumerable<Point> points)
        {
            var latitudes = points.Select(p => p.Latitude).ToArray();
            var maxLatitude = latitudes.Any() ? latitudes.Max() : 0;
            var minLatitude = latitudes.Any() ? latitudes.Min() : 0;
            return (maxLatitude + minLatitude)/2;
        }

        private static double GetCenterLongitude(IEnumerable<Point> points)
        {
            var longitudes = points.Select(p => p.Longitude).ToArray();
            var maxLongitude = longitudes.Any() ? longitudes.Max() : 0;
            var minLongitude = longitudes.Any() ? longitudes.Min() : 0;
            return (maxLongitude + minLongitude)/2;
        }
    }
}
