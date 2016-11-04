using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Models;

namespace Fakes.Datacards
{
    public class DatacardFake : IDatacard
    {
        private Plugin[] _plugins;
        public string Id { get; }
       

        public DatacardFake(string id)
        {
            Id = id;
        }

        public Task<Plugin[]> GetPlugins()
        {
            return Task.FromResult(_plugins);
        }

        public void SetupPlugins(Plugin[] plugins)
        {
            _plugins = plugins;
        }
    }
}
