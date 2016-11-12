using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Adapt.Analyzer.Api
{
    public class HttpConfigFactory
    {
        public static HttpConfiguration Create()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return config;
        }
    }
}