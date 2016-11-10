using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Adapt.Analyzer.Core.Datacards.Metadata
{
    public class Metadata
    {
        public ApplicationDataModel[] DataModels { get; private set; }

        public Metadata(IEnumerable<ApplicationDataModel> dataModels)
        {
            DataModels = dataModels.ToArray();
        }
    }
}