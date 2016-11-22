using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;

namespace Adapt.Analyzer.Core
{
    public static class FieldBoundaryExtensions
    {
        public static IEnumerable<Polygon> GetPolygons(this FieldBoundary fieldBoundary)
        {
            return fieldBoundary.SpatialData?.Polygons ?? Enumerable.Empty<Polygon>();
        }
    }
}
