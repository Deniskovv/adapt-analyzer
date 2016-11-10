using System.IO;
using AgGateway.ADAPT.PluginManager;

namespace Adapt.Analyzer.Core.General
{
    public class PluginFactory
    {
        public static IPluginFactory Create()
        {
            return new AgGateway.ADAPT.PluginManager.PluginFactory(Directory.GetCurrentDirectory());
        }
    }
}
