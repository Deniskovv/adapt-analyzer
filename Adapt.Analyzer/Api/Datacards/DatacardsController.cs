using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards
{
    [RoutePrefix("datacards")]
    public class DatacardsController : ApiController
    {
        private readonly IDatacard _datacard;

        public DatacardsController()
            : this(new Datacard())
        {
            
        }

        public DatacardsController(IDatacard datacard)
        {
            _datacard = datacard;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetDatacards()
        {
            var datacards = await _datacard.GetDatacards();
            return Ok(datacards);
        }
    }
}