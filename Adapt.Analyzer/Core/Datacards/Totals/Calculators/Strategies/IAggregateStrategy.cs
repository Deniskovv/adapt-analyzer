using Adapt.Analyzer.Core.Datacards.Totals.Models;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;

namespace Adapt.Analyzer.Core.Datacards.Totals.Calculators.Strategies
{
    public interface IAggregateStrategy
    {
        void Include(SpatialRecord record);
        RepresentationTotal Calculate();
    }
}