using System.IO;
using Adapt.Analyzer.Api;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Adapt.Analyzer.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll)
                .UseErrorPage()
                .UseWebApi(HttpConfigFactory.Create())
                .UseFileServer(new FileServerOptions
                {
                    EnableDefaultFiles = true,
                    FileSystem = new PhysicalFileSystem(Path.Combine(Directory.GetCurrentDirectory()))
                });
        }
    }
}
