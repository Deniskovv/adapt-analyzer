using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Metadata
{
    [RoutePrefix("datacards")]
    public class MetadataController : ApiController
    {
        private readonly IDatacard _datacard;

        public MetadataController()
            : this(new Datacard())
        {
            
        }

        public MetadataController(IDatacard datacard)
        {
            _datacard = datacard;
        }

        [HttpGet]
        [Route("{datacardId}/metadata")]
        public async Task<IHttpActionResult> GetMetadata(string datacardId)
        {
            var metadata = await _datacard.GetMetadata(datacardId);
            return Ok(metadata);
        }
    }
}