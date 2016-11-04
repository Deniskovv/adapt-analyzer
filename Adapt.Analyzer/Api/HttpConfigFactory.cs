using System.Web.Http;

namespace Adapt.Analzyer.Api
{
    public class HttpConfigFactory
    {
        public static HttpConfiguration Create()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            return config;
        }
    }
}