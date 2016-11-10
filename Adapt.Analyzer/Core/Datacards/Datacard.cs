using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Plugins;

namespace Adapt.Analyzer.Core.Datacards
{
    public interface IDatacard
    {
        string Id { get; }
        Task<Plugin[]> GetPlugins();
        Task<Metadata.Metadata> GetMetadata();
    }

    public class Datacard : IDatacard
    {
        private readonly IDatacardPluginFinder _datacardPluginFinder;
        private readonly IDatacardMetadataReader _datacardMetadataReader;
        
        public string Id { get; }

        public Datacard(string id)
            : this(id, new DatacardPluginFinder(), new DatacardMetadataReader())
        {
            
        }

        public Datacard(string id, IDatacardPluginFinder datacardPluginFinder, IDatacardMetadataReader datacardMetadataReader)
        {
            Id = id;
            _datacardPluginFinder = datacardPluginFinder;
            _datacardMetadataReader = datacardMetadataReader;
        }

        public Task<Plugin[]> GetPlugins()
        {
            return _datacardPluginFinder.GetPlugins(Id);
        }

        public Task<Metadata.Metadata> GetMetadata()
        {
            return _datacardMetadataReader.Read(Id);
        }
    }
}