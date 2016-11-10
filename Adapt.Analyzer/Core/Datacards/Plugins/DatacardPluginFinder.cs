using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Extract;
using Adapt.Analyzer.Core.Datacards.Models;
using AgGateway.ADAPT.PluginManager;
using PluginFactory = Adapt.Analyzer.Core.General.PluginFactory;

namespace Adapt.Analyzer.Core.Datacards.Plugins
{
    public interface IDatacardPluginFinder
    {
        Task<Plugin[]> GetPlugins(string id);
    }

    public class DatacardPluginFinder : IDatacardPluginFinder
    {
        private readonly IPluginFactory _pluginFactory;
        private readonly IDatacardExtractor _datacardExtractor;

        public DatacardPluginFinder()
            : this(new DatacardExtractor(), PluginFactory.Create())
        {
            
        }

        public DatacardPluginFinder(IDatacardExtractor datacardExtractor, IPluginFactory pluginFactory)
        {
            _datacardExtractor = datacardExtractor;
            _pluginFactory = pluginFactory;
        }


        public Task<Plugin[]> GetPlugins(string id)
        {
            var datacardPath = _datacardExtractor.Extract(id);

            var plugins = GetSupportedPlugins(datacardPath);
            return Task.FromResult(plugins);
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
