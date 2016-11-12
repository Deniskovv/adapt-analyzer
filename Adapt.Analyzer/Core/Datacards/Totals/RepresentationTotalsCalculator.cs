using System.Linq;
using System.Threading.Tasks;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;

namespace Adapt.Analyzer.Core.Datacards.Totals
{
    public interface IRepresentationTotalsCalculator
    {
        Task<RepresentationTotal[]> Calculate(OperationData operationData);
    }

    public class RepresentationTotalsCalculator : IRepresentationTotalsCalculator
    {
        public Task<RepresentationTotal[]> Calculate(OperationData operationData)
        {
            var representationTotals = operationData.GetNumericWorkingData()
                .Select(d => new RepresentationTotal(null, null, 0))
                .ToArray();

            return Task.FromResult(representationTotals);
        }
    }
}