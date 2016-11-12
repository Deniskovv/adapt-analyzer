namespace Adapt.Analyzer.Core.Datacards.Totals.Models
{
    public class DatacardTotals
    {
        public PluginTotals[] PluginTotals { get; }

        public DatacardTotals(PluginTotals[] pluginTotals)
        {
            PluginTotals = pluginTotals;
        }
    }
}
