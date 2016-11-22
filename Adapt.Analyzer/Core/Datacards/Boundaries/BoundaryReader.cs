using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;
using FieldBoundary = AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries.FieldBoundary;

namespace Adapt.Analyzer.Core.Datacards.Boundaries
{
    public interface IBoundaryReader
    {
        Task<Boundary[]> GetBoundaries(IEnumerable<FieldBoundary> fieldBoundaries);
    }

    public class BoundaryReader : IBoundaryReader
    {
        public Task<Boundary[]> GetBoundaries(IEnumerable<FieldBoundary> fieldBoundaries)
        {
            var boundaries = fieldBoundaries.Select(CreateBoundary).ToArray();
            return Task.FromResult(boundaries);
        }

        private static Boundary CreateBoundary(FieldBoundary fieldBoundary)
        {
            var exteriors = GetExteriors(fieldBoundary);
            var interiors = GetInteriors(fieldBoundary);
            return new Boundary(exteriors, interiors);
        }

        private static Ring[] GetExteriors(FieldBoundary fieldBoundary)
        {
            return fieldBoundary.GetPolygons()
                .Select(p => p.ExteriorRing)
                .Select(Ring.FromLinearRing)
                .ToArray();
        }

        private static Ring[] GetInteriors(FieldBoundary fieldBoundary)
        {
            return fieldBoundary.GetPolygons()
                .SelectMany(p => p.InteriorRings)
                .Select(Ring.FromLinearRing)
                .ToArray();
        }
    }
}
