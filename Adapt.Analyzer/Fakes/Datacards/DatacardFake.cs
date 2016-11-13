using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Totals.Models;

namespace Fakes.Datacards
{
    public class DatacardFake : IDatacard
    {
        private Plugin[] _plugins;
        private Metadata _metadata;
        private DatacardTotals _datacardTotals;
        public string Id { get; }
       

        public DatacardFake(string id)
        {
            Id = id;
        }

        public Task<Plugin[]> GetPlugins()
        {
            return Task.FromResult(_plugins);
        }

        public Task<Metadata> GetMetadata()
        {
            return Task.FromResult(_metadata);
        }

        public Task<DatacardTotals> CalculateTotals()
        {
            return Task.FromResult(_datacardTotals);
        }

        public void SetupPlugins(Plugin[] plugins)
        {
            _plugins = plugins;
        }

        public void SetupMetadata(Metadata metadata)
        {
            _metadata = metadata;
        }

        public void SetupTotals(DatacardTotals totals)
        {
            _datacardTotals = totals;
        }
    }
}
