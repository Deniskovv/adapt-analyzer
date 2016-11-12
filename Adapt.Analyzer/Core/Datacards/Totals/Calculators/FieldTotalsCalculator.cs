using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Totals.Models;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;

namespace Adapt.Analyzer.Core.Datacards.Totals.Calculators
{
    public interface IFieldTotalsCalculator
    {
        Task<FieldTotals[]> Calculate(IPlugin plugin, string datacardPath);
    }

    public class FieldTotalsCalculator : IFieldTotalsCalculator
    {
        private readonly IOperationTotalsCalculator _operationTotalsCalculator;

        public FieldTotalsCalculator()
            : this(new OperationTotalsCalculator())
        {
            
        }

        public FieldTotalsCalculator(IOperationTotalsCalculator operationTotalsCalculator)
        {
            _operationTotalsCalculator = operationTotalsCalculator;
        }

        public async Task<FieldTotals[]> Calculate(IPlugin plugin, string datacardPath)
        {
            var dataModels = plugin.Import(datacardPath);
            return await CalculateFieldTotals(dataModels);
        }

        private async Task<FieldTotals[]> CalculateFieldTotals(IEnumerable<ApplicationDataModel> dataModels)
        {
            var fieldTotals = new List<FieldTotals>();
            foreach (var dataModel in dataModels)
                fieldTotals.AddRange(await CalculateFieldTotals(dataModel));
            return fieldTotals.ToArray();
        }

        private async Task<IEnumerable<FieldTotals>> CalculateFieldTotals(ApplicationDataModel dataModel)
        {
            var fieldTotals = new List<FieldTotals>();
            foreach (var field in dataModel.GetFields())
                if(dataModel.HasLoggedDataForField(field))
                    fieldTotals.Add(await CalculateFieldTotals(field, dataModel));
            return fieldTotals;
        }

        private async Task<FieldTotals> CalculateFieldTotals(Field catalogField, ApplicationDataModel dataModel)
        {
            var operationTotals = await _operationTotalsCalculator.Calculate(catalogField, dataModel);
            return new FieldTotals(catalogField.Description, operationTotals);
        }
    }
}
