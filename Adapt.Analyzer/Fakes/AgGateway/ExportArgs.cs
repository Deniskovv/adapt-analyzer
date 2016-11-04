using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Fakes.AgGateway
{
    public class ExportArgs
    {
        public ApplicationDataModel DataModel { get; }
        public string ExportPath { get; }
        public Properties Properties { get; }

        public ExportArgs(ApplicationDataModel dataModel, string exportPath, Properties properties)
        {
            DataModel = dataModel;
            ExportPath = exportPath;
            Properties = properties;
        }
    }
}