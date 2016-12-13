using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Adapt.Analyzer.Core.General
{
    public interface ISerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string value);
    }

    public class Serializer : ISerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public Serializer()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, _serializerSettings);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _serializerSettings);
        }
    }
}
