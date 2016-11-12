using System.Collections.Generic;
using System.Linq;
using Adapt.Analyzer.Core.Datacards.Totals.Models;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;

namespace Adapt.Analyzer.Core.Datacards.Totals.Calculators.Strategies
{
    public class AverageAggregateStrategy : IAggregateStrategy
    {
        private readonly NumericWorkingData _workingData;
        private readonly List<double> _spatialValues;
        public AverageAggregateStrategy(NumericWorkingData workingData)
        {
            _workingData = workingData;
            _spatialValues = new List<double>();
        }

        public void Include(SpatialRecord record)
        {
            _spatialValues.Add(record.GetNumericMeterValue(_workingData));
        }

        public RepresentationTotal Calculate()
        {
            var average = _spatialValues.Count > 0 ? _spatialValues.Average() : 0;
            return new RepresentationTotal(_workingData.Representation.Code, _workingData.UnitOfMeasure.Code, average);
        }
    }
}