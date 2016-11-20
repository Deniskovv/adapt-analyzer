using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Fakes.Owin
{
    public class OwinRequestFake : IOwinRequest
    {
        public IDictionary<string, object> Environment { get; }
        public IOwinContext Context { get; set; }
        public string Method { get; set; }
        public string Scheme { get; set; }
        public bool IsSecure { get; set; }
        public HostString Host { get; set; }
        public PathString PathBase { get; set; }
        public PathString Path { get; set; }
        public QueryString QueryString { get; set; }
        public IReadableStringCollection Query { get; set; }
        public Uri Uri { get; set; }
        public string Protocol { get; set; }
        public IHeaderDictionary Headers { get; set; }
        public RequestCookieCollection Cookies { get; set; }
        public string ContentType { get; set; }
        public string CacheControl { get; set; }
        public string MediaType { get; set; }
        public string Accept { get; set; }
        public Stream Body { get; set; }
        public CancellationToken CallCancelled { get; set; }
        public string LocalIpAddress { get; set; }
        public int? LocalPort { get; set; }
        public string RemoteIpAddress { get; set; }
        public int? RemotePort { get; set; }
        public IPrincipal User { get; set; }

        public OwinRequestFake()
        {
            Environment = new ConcurrentDictionary<string, object>();
        }

        public T Get<T>(string key)
        {
            return (T) Environment[key];
        }

        public Task<IFormCollection> ReadFormAsync()
        {
            throw new NotImplementedException();
        }

        public IOwinRequest Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }
    }
}
