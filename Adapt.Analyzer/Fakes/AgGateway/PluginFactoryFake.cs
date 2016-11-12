using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.PluginManager;

namespace Fakes.AgGateway
{
    public class PluginFactoryFake : IPluginFactory
    {
        public List<IPlugin> Plugins { get; }

        public List<string> AvailablePlugins
        {
            get { return Plugins.Select(p => p.Name).ToList(); }
        }

        public PluginFactoryFake()
        {
            Plugins = new List<IPlugin>();
        }

        public IPlugin GetPlugin(string pluginName)
        {
            return Plugins.Single(p => p.Name == pluginName);
        }

        public List<IPlugin> GetSupportedPlugins(string dataPath, Properties properties = null)
        {
            return Plugins.Where(p => p.IsDataCardSupported(dataPath, properties)).ToList();
        }

        public PredicatePlugin AddUnsupportedPlugin(string name = "nope", string version = "jpg")
        {
            return AddPlugin(name, version, false);
        }

        public PredicatePlugin AddSupportedPlugin(string name = "what", string version = "nope")
        {
            return AddPlugin(name, version, true);
        }

        private PredicatePlugin AddPlugin(string name, string version, bool isSupported)
        {
            var plugin = new PredicatePlugin((s, p) => isSupported)
            {
                Name = name,
                Version = version
            };
            Plugins.Add(plugin);
            return plugin;
        }
    }
}
