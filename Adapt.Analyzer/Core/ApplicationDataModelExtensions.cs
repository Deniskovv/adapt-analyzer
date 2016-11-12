using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;

namespace Adapt.Analyzer.Core
{
    public static class ApplicationDataModelExtensions
    {
        public static bool HasLoggedDataForField(this ApplicationDataModel dataModel, Field field)
        {
            return dataModel.Documents.LoggedData
                .Any(l => l.FieldId == field.Id.ReferenceId);
        }

        public static IEnumerable<Field> GetFields(this ApplicationDataModel dataModel)
        {
            return dataModel.Catalog == null 
                ? Enumerable.Empty<Field>() 
                : dataModel.Catalog.Fields;
        }

        public static IEnumerable<LoggedData> GetLoggedData(this ApplicationDataModel dataModel)
        {
            return dataModel.Documents == null 
                ? Enumerable.Empty<LoggedData>() 
                : dataModel.Documents.LoggedData;
        }

        public static IEnumerable<OperationData> GetOperationDataForField(this ApplicationDataModel dataModel,
            Field field)
        {
            return dataModel.GetLoggedData()
                .Where(l => l.FieldId == field.Id.ReferenceId)
                .Where(l => l.OperationData != null)
                .SelectMany(l => l.OperationData);
        }
    }
}
