using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Adapt.Analyzer.Core.Datacards.Boundaries
{
    public interface IFieldBoundaryReader
    {
        Task<FieldBoundary[]> GetFieldBoundaries(IEnumerable<StorageDataModel> storageDataModels);
    }

    public class FieldBoundaryReader : IFieldBoundaryReader
    {
        public Task<FieldBoundary[]> GetFieldBoundaries(IEnumerable<StorageDataModel> storageDataModels)
        {
            var fieldBoundaries = storageDataModels.SelectMany(GetFieldBoundaries).ToArray();
            return Task.FromResult(fieldBoundaries);
        }

        private static IEnumerable<FieldBoundary> GetFieldBoundaries(StorageDataModel dataModel)
        {
            return dataModel.DataModels
                .SelectMany(GetFieldBoundaries);
        }

        private static IEnumerable<FieldBoundary> GetFieldBoundaries(ApplicationDataModel dataModel)
        {
            return dataModel.Catalog
                .Fields.Select(f => new FieldBoundary());
        }
    }
}
