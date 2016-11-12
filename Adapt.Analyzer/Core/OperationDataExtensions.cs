using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;

namespace Adapt.Analyzer.Core
{
    public static class OperationDataExtensions
    {
        public static IEnumerable<WorkingData> GetWorkingData(this OperationData operationData)
        {
            return Enumerable.Range(0, operationData.MaxDepth)
                .SelectMany(i => operationData.GetDeviceElementUses(i))
                .SelectMany(e => e.GetWorkingDatas());
        }

        public static IEnumerable<NumericWorkingData> GetNumericWorkingData(this OperationData operationData)
        {
            return operationData.GetWorkingData()
                .OfType<NumericWorkingData>();
        }
    }
}
