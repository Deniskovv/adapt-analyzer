using System.IO;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Adapt.Analzyer.Api.Startup))]

namespace Adapt.Analzyer.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(CreateConfig())
                .UseFileServer(new FileServerOptions
                {
                    EnableDefaultFiles = true,
                    FileSystem = new PhysicalFileSystem(Path.Combine(Directory.GetCurrentDirectory()))
                });
        }

        private static HttpConfiguration CreateConfig()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            return config;
        }
    }
}
