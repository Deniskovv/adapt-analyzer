using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analzyer.Api.Datacards.Plugins
{
    [RoutePrefix("datacards")]
    public class PluginsController : ApiController
    {
        private readonly IDatacardFactory _datacardFactory;

        public PluginsController()
            : this(new DatacardFactory())
        {
            
        }

        public PluginsController(IDatacardFactory datacardFactory)
        {
            _datacardFactory = datacardFactory;
        }

        [HttpGet]
        [Route("{datacardId}/plugins")]
        public async Task<IHttpActionResult> GetPlugins(string datacardId)
        {
            var datacard = _datacardFactory.Create(datacardId);
            var plugins = await datacard.GetPlugins();
            return Ok(plugins);
        }
    }
}