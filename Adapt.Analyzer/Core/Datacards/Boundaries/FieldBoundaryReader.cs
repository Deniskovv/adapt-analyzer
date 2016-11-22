using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;

namespace Adapt.Analyzer.Core.Datacards.Boundaries
{
    public interface IFieldBoundaryReader
    {
        Task<FieldBoundary[]> GetFieldBoundaries(IEnumerable<StorageDataModel> storageDataModels);
    }

    public class FieldBoundaryReader : IFieldBoundaryReader
    {
        private readonly IBoundaryReader _boundaryReader;

        public FieldBoundaryReader()
            : this(new BoundaryReader())
        {
            
        }

        public FieldBoundaryReader(IBoundaryReader boundaryReader)
        {
            _boundaryReader = boundaryReader;
        }

        public async Task<FieldBoundary[]> GetFieldBoundaries(IEnumerable<StorageDataModel> storageDataModels)
        {
            var boundaries = await storageDataModels
                .Select(GetFieldBoundaries)
                .Flatten();
            return boundaries.ToArray();
        }

        private async Task<FieldBoundary[]> GetFieldBoundaries(StorageDataModel dataModel)
        {
            var boundaries = await dataModel.DataModels
                .Select(GetFieldBoundaries)
                .Flatten();
            return boundaries.ToArray();
        }

        private async Task<FieldBoundary[]> GetFieldBoundaries(ApplicationDataModel dataModel)
        {
            var fieldBoundaries = dataModel.Catalog.Fields
                .Where(f => HasBoundary(f, dataModel.Catalog.FieldBoundaries))
                .Select(async f => await CreateFieldBoundary(f, dataModel.GetFieldBoundaries(f)))
                .ToArray();
            return await Task.WhenAll(fieldBoundaries);
        }

        private static bool HasBoundary(Field field, IEnumerable<AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries.FieldBoundary> boundaries)
        {
            return boundaries.Any(b => b.FieldId == field.Id.ReferenceId);
        }

        private async Task<FieldBoundary> CreateFieldBoundary(Field field, IEnumerable<AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries.FieldBoundary> fieldBoundaries)
        {
            var boundaries = await _boundaryReader.GetBoundaries(fieldBoundaries);
            return new FieldBoundary(field.Description, boundaries);
        }
    }
}
