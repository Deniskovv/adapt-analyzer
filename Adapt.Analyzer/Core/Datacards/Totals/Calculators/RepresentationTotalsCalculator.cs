using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Totals.Calculators.Strategies;
using Adapt.Analyzer.Core.Datacards.Totals.Models;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;

namespace Adapt.Analyzer.Core.Datacards.Totals.Calculators
{
    public interface IRepresentationTotalsCalculator
    {
        Task<RepresentationTotal[]> Calculate(List<NumericWorkingData> numericWorkingData, List<SpatialRecord> spatialRecords);
    }

    public class RepresentationTotalsCalculator : IRepresentationTotalsCalculator
    {
        private readonly IAggregateStrategyFactory _aggregateStrategyFactory;

        public RepresentationTotalsCalculator()
            : this(new AggregateStrategyFactory())
        {
            
        }

        public RepresentationTotalsCalculator(IAggregateStrategyFactory aggregateStrategyFactory)
        {
            _aggregateStrategyFactory = aggregateStrategyFactory;
        }

        public Task<RepresentationTotal[]> Calculate(List<NumericWorkingData> numericWorkingData, List<SpatialRecord> spatialRecords)
        {
            var aggregateStategies = numericWorkingData.Select(d => _aggregateStrategyFactory.GetStrategy(d)).ToArray();

            foreach (var spatialRecord in spatialRecords)
                foreach (var aggregateStrategy in aggregateStategies)
                    aggregateStrategy.Include(spatialRecord);

            var totals = aggregateStategies.Select(s => s.Calculate()).ToArray();
            return Task.FromResult(totals);
        }
    }
}