using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Fakes.Owin.Middleware
{
    public class OwinMiddlewareFake : OwinMiddleware
    {
        public IOwinContext Context { get; private set; }

        public Exception Exception { get; set; }

        public OwinMiddlewareFake() 
            : base(null)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            Context = context;
            if (Exception != null)
                throw Exception;
            return Task.CompletedTask;
        }
    }
}
