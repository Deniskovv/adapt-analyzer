using AgGateway.ADAPT.ApplicationDataModel.Shapes;

namespace Adapt.Analyzer.Core.Test.Adapt.Helpers
{
    public class PointFactory
    {
        public static Point CreatePoint(double x, double y)
        {
            return new Point
            {
                X = x,
                Y = y
            };
        }
    }
}