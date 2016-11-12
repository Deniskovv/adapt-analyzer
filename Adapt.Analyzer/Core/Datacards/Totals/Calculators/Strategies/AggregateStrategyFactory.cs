using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;

namespace Adapt.Analyzer.Core.Datacards.Totals.Calculators.Strategies
{
    public interface IAggregateStrategyFactory
    {
        IAggregateStrategy GetStrategy(NumericWorkingData workingData);
    }

    public class AggregateStrategyFactory : IAggregateStrategyFactory
    {
        private readonly UnitOfMeasureDimensionEnum[] _ratioDimensions = {
            UnitOfMeasureDimensionEnum.Percent
        };

        public IAggregateStrategy GetStrategy(NumericWorkingData workingData)
        {
            if (IsRatioUnitOfMeasure(workingData))
            {
                return new AverageAggregateStrategy(workingData);   
            }
            return new SumAggregateStrategy(workingData);
        }

        private bool IsRatioUnitOfMeasure(NumericWorkingData workingData)
        {
            return _ratioDimensions.Contains(workingData.UnitOfMeasure.Dimension);
        }
    }
}
