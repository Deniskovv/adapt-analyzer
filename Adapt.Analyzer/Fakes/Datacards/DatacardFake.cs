using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Plugins.Models;
using Adapt.Analyzer.Core.Datacards.Totals.Models;

namespace Fakes.Datacards
{
    public class DatacardFake : IDatacard
    {
        private Plugin[] _plugins;
        private Metadata _metadata;
        private DatacardTotals _datacardTotals;


        public DatacardFake()
        {
        }

        public Task<Plugin[]> GetPlugins(string id)
        {
            return Task.FromResult(_plugins);
        }

        public Task<Metadata> GetMetadata(string id)
        {
            return Task.FromResult(_metadata);
        }

        public Task<DatacardTotals> CalculateTotals(string id)
        {
            return Task.FromResult(_datacardTotals);
        }

        public Task<FieldBoundary[]> GetFieldBoundaries(string id)
        {
            throw new System.NotImplementedException();
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
