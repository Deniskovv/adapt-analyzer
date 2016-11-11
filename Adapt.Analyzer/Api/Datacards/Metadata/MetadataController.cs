using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Metadata
{
    [RoutePrefix("datacards")]
    public class MetadataController : ApiController
    {
        private readonly IDatacardFactory _datacardFactory;

        public MetadataController()
            : this(new DatacardFactory())
        {
            
        }

        public MetadataController(IDatacardFactory datacardFactory)
        {
            _datacardFactory = datacardFactory;
        }

        [HttpGet]
        [Route("{datacardId}/metadata")]
        public async Task<IHttpActionResult> GetMetadata(string datacardId)
        {
            var datacard = _datacardFactory.Create(datacardId);
            var metadata = await datacard.GetMetadata();
            return Ok(metadata);
        }
    }
}