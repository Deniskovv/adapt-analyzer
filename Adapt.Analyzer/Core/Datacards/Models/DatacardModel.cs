using Newtonsoft.Json;

namespace Adapt.Analyzer.Core.Datacards.Models
{
    public class DatacardModel
    {
        public string Id { get; }
        public string Name { get; }

        [JsonIgnore]
        public byte[] Bytes { get; }

        public DatacardModel(string id = null, string name = null, byte[] bytes = null)
        {
            Id = id;
            Name = name;
            Bytes = bytes;
        }
    }
}
