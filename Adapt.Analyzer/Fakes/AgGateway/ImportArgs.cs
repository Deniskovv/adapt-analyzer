using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Fakes.AgGateway
{
    public class ImportArgs
    {
        public string DataPath { get; }
        public Properties Properties { get; }

        public ImportArgs(string dataPath, Properties properties)
        {
            DataPath = dataPath;
            Properties = properties;
        }
    }
}