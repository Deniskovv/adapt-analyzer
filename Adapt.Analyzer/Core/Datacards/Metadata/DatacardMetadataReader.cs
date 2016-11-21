using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Storage.Models;

namespace Adapt.Analyzer.Core.Datacards.Metadata
{
    public interface IDatacardMetadataReader
    {
        Task<Metadata> Read(IEnumerable<StorageDataModel> storageDataModels);
    }

    public class DatacardMetadataReader : IDatacardMetadataReader
    {
        public Task<Metadata> Read(IEnumerable<StorageDataModel> storageDataModels)
        {
            var dataModels = storageDataModels.SelectMany(Import);
            var metadata = new Metadata(dataModels);
            return Task.FromResult(metadata);
        }

        private static IEnumerable<PluginDataModel> Import(StorageDataModel dataModel)
        {
            return dataModel.DataModels
                .Select(d => new PluginDataModel(dataModel.PluginName, dataModel.PluginVersion, d));
        }
    }
}
