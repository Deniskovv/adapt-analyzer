using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Plugins.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Models;

namespace Adapt.Analyzer.Core.Datacards.Plugins
{
    public interface IDatacardPluginFinder
    {
        Task<Plugin[]> GetPlugins(IEnumerable<StorageDataModel> dataModels);
    }

    public class DatacardPluginFinder : IDatacardPluginFinder
    {
        public Task<Plugin[]> GetPlugins(IEnumerable<StorageDataModel> dataModels)
        {
            var plugins = dataModels.Select(d => new Plugin
            {
                Name = d.PluginName,
                Version = d.PluginVersion
            }).ToArray();
            return Task.FromResult(plugins);
        }
    }
}
