using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Fakes.Owin
{
    public class OwinContextFake : IOwinContext
    {
        public IOwinRequest Request { get; set; }
        public IOwinResponse Response { get; set; }
        public IAuthenticationManager Authentication { get; set; }
        public IDictionary<string, object> Environment { get; set; }
        public TextWriter TraceOutput { get; set; }

        public OwinContextFake()
        {
            Environment = new ConcurrentDictionary<string, object>();
        }

        public T Get<T>(string key)
        {
            return (T)Environment[key];
        }

        public IOwinContext Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }
    }
}
