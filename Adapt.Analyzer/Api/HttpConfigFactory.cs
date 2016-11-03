using System.Linq;
using System.Web.Http;
using Adapt.Analyzer.Core.IoC;
using Adapt.Analzyer.Api.IoC;

namespace Adapt.Analzyer.Api
{
    public class HttpConfigFactory
    {
        public static HttpConfiguration Create()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            var container = new Container(typeof(Container).Assembly, typeof(Startup).Assembly);

            RegisterWebApiControllers(config, container);

            config.DependencyResolver = new DependencyResolver(container);
            return config;
        }

        private static void RegisterWebApiControllers(HttpConfiguration config, Container container)
        {
            var assemblyResolver = config.Services.GetAssembliesResolver();
            var controllerTypes = config.Services.GetHttpControllerTypeResolver().GetControllerTypes(assemblyResolver);
            container.RegisterTypes(controllerTypes.ToArray());
        }
    }
}