namespace Adapt.Analyzer.Core.Datacards.Totals
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
