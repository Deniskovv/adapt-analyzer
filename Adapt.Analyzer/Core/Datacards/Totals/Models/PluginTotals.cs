namespace Adapt.Analyzer.Core.Datacards.Totals.Models
{
    public class PluginTotals
    {
        public FieldTotals[] FieldTotals { get; }
        public string PluginName { get; }
        public string PluginVersion { get; }

        public PluginTotals(string pluginName, string pluginVersion, FieldTotals[] fieldTotals)
        {
            FieldTotals = fieldTotals;
            PluginVersion = pluginVersion;
            PluginName = pluginName;
        }
    }
}
