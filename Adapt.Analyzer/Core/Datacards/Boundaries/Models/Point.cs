namespace Adapt.Analyzer.Core.Datacards.Boundaries.Models
{
    public class Point
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Point(double longitude, double latitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Point FromAdaptPoint(AgGateway.ADAPT.ApplicationDataModel.Shapes.Point point)
        {
            return new Point(point.X, point.Y);
        }
    }
}