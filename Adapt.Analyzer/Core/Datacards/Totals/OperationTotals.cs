using AgGateway.ADAPT.ApplicationDataModel.Common;

namespace Adapt.Analyzer.Core.Datacards.Totals
{
    public class OperationTotals
    {
        public OperationTypeEnum OperationType { get; }
        public RepresentationTotal[] RepresentationTotals { get; }

        public OperationTotals(OperationTypeEnum operationType, RepresentationTotal[] representationTotals)
        {
            OperationType = operationType;
            RepresentationTotals = representationTotals;
        }
    }
}
