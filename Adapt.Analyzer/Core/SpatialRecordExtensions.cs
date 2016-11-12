using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Representations;

namespace Adapt.Analyzer.Core
{
    public static class SpatialRecordExtensions
    {
        public static void SetNumericMeterValue(this SpatialRecord record, NumericWorkingData workingData, double value)
        {
            var numericRepresentation = workingData.Representation as NumericRepresentation;
            var numericValue = new NumericValue(workingData.UnitOfMeasure, value);
            var representationValue = new NumericRepresentationValue(numericRepresentation, workingData.UnitOfMeasure, numericValue);
            record.SetMeterValue(workingData, representationValue);
        }

        public static double GetNumericMeterValue(this SpatialRecord record, NumericWorkingData workingData)
        {
            var representationValue = (NumericRepresentationValue)record.GetMeterValue(workingData);
            return representationValue.Value.Value;
        }
    }
}
