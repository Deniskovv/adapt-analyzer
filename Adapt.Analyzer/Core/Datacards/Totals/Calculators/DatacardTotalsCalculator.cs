using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.Datacards.Totals.Models;

namespace Adapt.Analyzer.Core.Datacards.Totals.Calculators
{
    public interface IDatacardTotalsCalculator
    {
        Task<DatacardTotals> Calculate(IEnumerable<StorageDataModel> storageDataModels);
    }

    public class DatacardTotalsCalculator : IDatacardTotalsCalculator
    {
        private readonly IFieldTotalsCalculator _fieldTotalsCalculator;

        public DatacardTotalsCalculator()
            : this(new FieldTotalsCalculator())
        {
            
        }

        public DatacardTotalsCalculator(IFieldTotalsCalculator fieldTotalsCalculator)
        {
            _fieldTotalsCalculator = fieldTotalsCalculator;
        }

        public async Task<DatacardTotals> Calculate(IEnumerable<StorageDataModel> storageDataModels)
        {
            var pluginTotals = storageDataModels
                .Select(async s => await GetPluginTotals(s))
                .ToArray();

            return new DatacardTotals(await Task.WhenAll(pluginTotals));
        }

        public async Task<PluginTotals> GetPluginTotals(StorageDataModel storageDataModel)
        {
            var fieldTotals = await _fieldTotalsCalculator.Calculate(storageDataModel.DataModels);
            return new PluginTotals(storageDataModel.PluginName, storageDataModel.PluginVersion, fieldTotals);
        }
    }
}
