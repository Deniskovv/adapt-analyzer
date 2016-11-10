using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Extract;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Models;
using AgGateway.ADAPT.PluginManager;

namespace Adapt.Analyzer.Core.Datacards
{
    public interface IDatacard
    {
        string Id { get; }
        Task<Plugin[]> GetPlugins();
    }

    public class Datacard : IDatacard
    {
        private readonly IPluginFactory _pluginFactory;
        private readonly IDatacardExtractor _datacardExtractor;
        private readonly IDatacardMetadataReader _datacardMetadataReader;
        
        public string Id { get; }

        public Datacard(string id)
            : this(id, new DatacardExtractor(), new PluginFactory(Directory.GetCurrentDirectory()))
        {
            
        }

        public Datacard(string id, IDatacardExtractor datacardExtractor, IPluginFactory pluginFactory)
        {
            Id = id;
            _pluginFactory = pluginFactory;
            _datacardExtractor = datacardExtractor;
            _datacardMetadataReader = new DatacardMetadataReader(datacardExtractor, pluginFactory);
        }

        public Task<Plugin[]> GetPlugins()
        {
            var datacardPath = _datacardExtractor.Extract(Id);

            var plugins = GetSupportedPlugins(datacardPath);
            return Task.FromResult(plugins);
        }

        public Task<Metadata.Metadata> GetMetadata()
        {
            return _datacardMetadataReader.Read(Id);
        }

        private Plugin[] GetSupportedPlugins(string datacardPath)
        {
            return _pluginFactory.GetSupportedPlugins(datacardPath)
                .Select(p => new Plugin
                {
                    Name = p.Name,
                    Version = p.Version
                }).ToArray();
        }
    }
}