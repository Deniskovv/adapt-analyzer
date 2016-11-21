using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Storage.Extract;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Save;
using AgGateway.ADAPT.PluginManager;
using PluginFactory = Adapt.Analyzer.Core.General.PluginFactory;

namespace Adapt.Analyzer.Core.Datacards.Storage
{
    public interface IDatacardStorage
    {
        Task<string> Save(byte[] bytes);
        Task<StorageDataModel[]> GetDataModels(string id);
    }

    public class DatacardStorage : IDatacardStorage
    {
        private readonly IDatacardWriter _writer;
        private readonly IDatacardExtractor _extractor;
        private readonly IPluginFactory _pluginFactory;

        public DatacardStorage()
            : this(new DatacardWriter(), new DatacardExtractor(), PluginFactory.Create())
        {
            
        }

        public DatacardStorage(IDatacardWriter writer, IDatacardExtractor extractor, IPluginFactory pluginFactory)
        {
            _writer = writer;
            _extractor = extractor;
            _pluginFactory = pluginFactory;
        }

        public Task<string> Save(byte[] bytes)
        {
            return _writer.Write(bytes);
        }

        public Task<StorageDataModel[]> GetDataModels(string id)
        {
            var datacardPath = _extractor.Extract(id);
            var plugins = _pluginFactory.GetSupportedPlugins(datacardPath);
            var storageModels = plugins.Select(p => new StorageDataModel(datacardPath, p));
            return Task.FromResult(storageModels.ToArray());
        }
    }
}
