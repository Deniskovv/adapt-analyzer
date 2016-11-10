using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Extract;
using AgGateway.ADAPT.PluginManager;
using PluginFactory = Adapt.Analyzer.Core.General.PluginFactory;

namespace Adapt.Analyzer.Core.Datacards.Metadata
{
    public interface IDatacardMetadataReader
    {
        Task<Metadata> Read(string id);
    }

    public class DatacardMetadataReader : IDatacardMetadataReader
    {
        private readonly IDatacardExtractor _datacardExtractor;
        private readonly IPluginFactory _pluginFactory;

        public DatacardMetadataReader()
            : this(new DatacardExtractor(), PluginFactory.Create())
        {
            
        }

        public DatacardMetadataReader(IDatacardExtractor datacardExtractor, IPluginFactory pluginFactory)
        {
            _datacardExtractor = datacardExtractor;
            _pluginFactory = pluginFactory;
        }

        public Task<Metadata> Read(string id)
        {
            var datacardPath = _datacardExtractor.Extract(id);
            var plugins = _pluginFactory.GetSupportedPlugins(datacardPath)[0];
            var dataModels = plugins.Import(datacardPath);
            var metadata = new Metadata(dataModels);
            return Task.FromResult(metadata);
        }
    }
}
