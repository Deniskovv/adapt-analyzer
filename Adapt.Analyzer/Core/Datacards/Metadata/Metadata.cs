using System.Collections.Generic;
using System.Linq;

namespace Adapt.Analyzer.Core.Datacards.Metadata
{
    public class Metadata
    {
        public PluginDataModel[] DataModels { get; }

        public Metadata(IEnumerable<PluginDataModel> dataModels)
        {
            DataModels = dataModels.ToArray();
        }
    }
}