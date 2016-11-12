using System;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Extract;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.PluginManager;
using PluginFactory = Adapt.Analyzer.Core.General.PluginFactory;

namespace Adapt.Analyzer.Core.Datacards.Totals
{
    public interface IDatacardTotalsCalculator
    {
        Task<DatacardTotals> Calculate(string datacardId);
    }

    public class DatacardTotalsCalculator : IDatacardTotalsCalculator
    {
        private readonly IDatacardExtractor _datacardExtractor;
        private readonly IPluginFactory _pluginFactory;
        private readonly IFieldTotalsCalculator _fieldTotalsCalculator;

        public DatacardTotalsCalculator()
            : this(new DatacardExtractor(), PluginFactory.Create(), new FieldTotalsCalculator())
        {
            
        }

        public DatacardTotalsCalculator(IDatacardExtractor datacardExtractor, IPluginFactory pluginFactory, IFieldTotalsCalculator fieldTotalsCalculator)
        {
            _datacardExtractor = datacardExtractor;
            _pluginFactory = pluginFactory;
            _fieldTotalsCalculator = fieldTotalsCalculator;
        }

        public async Task<DatacardTotals> Calculate(string datacardId)
        {
            var datacardPath = _datacardExtractor.Extract(datacardId);
            var pluginTotals = _pluginFactory.GetSupportedPlugins(datacardPath)
                .Select(async p => await GetPluginTotals(p, datacardPath))
                .ToArray();

            return new DatacardTotals(await Task.WhenAll(pluginTotals));
        }

        public async Task<PluginTotals> GetPluginTotals(IPlugin plugin, string datacardPath)
        {
            var fieldTotals = await _fieldTotalsCalculator.Calculate(plugin, datacardPath);
            return new PluginTotals(plugin.Name, plugin.Version, fieldTotals);
        }
    }
}
