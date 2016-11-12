using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Extract;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
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
            var plugins = _pluginFactory.GetSupportedPlugins(datacardPath);
            var dataModels = plugins.SelectMany(p => Import(p, datacardPath));
            var metadata = new Metadata(dataModels);
            return Task.FromResult(metadata);
        }

        private IEnumerable<PluginDataModel> Import(IPlugin plugin, string datacardPath)
        {
            return plugin.Import(datacardPath)
                .Select(d => new PluginDataModel(plugin.Name, plugin.Version, d));
        }
    }
}
