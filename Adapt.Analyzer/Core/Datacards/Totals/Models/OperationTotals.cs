using AgGateway.ADAPT.ApplicationDataModel.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Adapt.Analyzer.Core.Datacards.Totals.Models
{
    public class OperationTotals
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationTypeEnum OperationType { get; }
        public RepresentationTotal[] RepresentationTotals { get; }

        public OperationTotals(OperationTypeEnum operationType, RepresentationTotal[] representationTotals)
        {
            OperationType = operationType;
            RepresentationTotals = representationTotals;
        }
    }
}
