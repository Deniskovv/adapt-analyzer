using System.Threading.Tasks;
using System.Web.Http;

namespace Adapt.Analyzer.Api
{
    [RoutePrefix("hello")]
    public class HelloController : ApiController
    {
        [HttpGet]
        [Route("")]
        public Task<IHttpActionResult> Get()
        {
            return Task.FromResult<IHttpActionResult>(Ok());
        }
    }
}