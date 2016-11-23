using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Plugins
{
    [RoutePrefix("datacards")]
    public class PluginsController : ApiController
    {
        private readonly IDatacard _datacard;

        public PluginsController()
            : this(new Datacard())
        {
            
        }

        public PluginsController(IDatacard datacard)
        {
            _datacard = datacard;
        }

        [HttpGet]
        [Route("{datacardId}/plugins")]
        public async Task<IHttpActionResult> GetPlugins(string datacardId)
        {
            var plugins = await _datacard.GetPlugins(datacardId);
            return Ok(plugins);
        }
    }
}