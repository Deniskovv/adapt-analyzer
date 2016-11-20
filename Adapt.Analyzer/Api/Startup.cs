using System.IO;
using Adapt.Analyzer.Api;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using Adapt.Analyzer.Api.Middleware;

[assembly: OwinStartup(typeof(Startup))]

namespace Adapt.Analyzer.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            app.Use<LoggingMiddleware>()
                .UseCors(CorsOptions.AllowAll)
                .UseErrorPage()
                .UseWebApi(HttpConfigFactory.Create())
                .UseFileServer(new FileServerOptions
                {
                    EnableDefaultFiles = true,
                    FileSystem = new PhysicalFileSystem(Directory.GetCurrentDirectory())
                })
                .UseStaticFiles(new StaticFileOptions
                {
                    ServeUnknownFileTypes = true
                });
        }
    }
}
