using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Adapt.Analyzer.Core.Datacards.Storage.Models
{
    public class StorageDataModel
    {
        private readonly string _path;
        private readonly IPlugin _plugin;
        private ApplicationDataModel[] _dataModels;

        public string PluginName => _plugin.Name;
        public string PluginVersion => _plugin.Version;

        public ApplicationDataModel[] DataModels => _dataModels ?? (_dataModels = _plugin.Import(_path).ToArray());

        public StorageDataModel(string path, IPlugin plugin)
        {
            _path = path;
            _plugin = plugin;
        }
    }
}
