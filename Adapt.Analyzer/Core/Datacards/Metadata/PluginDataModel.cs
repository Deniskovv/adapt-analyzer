using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Adapt.Analyzer.Core.Datacards.Metadata
{
    public class PluginDataModel
    {
        public string PluginName { get; }
        public string PluginVersion { get; }
        public ApplicationDataModel DataModel { get; }

        public PluginDataModel(string pluginName, string pluginVersion, ApplicationDataModel dataModel)
        {
            PluginName = pluginName;
            PluginVersion = pluginVersion;
            DataModel = dataModel;
        }
    }
}
