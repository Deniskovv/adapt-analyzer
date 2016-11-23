using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Boundaries
{
    public class BoundariesController : ApiController
    {
        private readonly IDatacard _datacard;

        public BoundariesController()
            : this(new Datacard())
        {
            
        }
            
        public BoundariesController(IDatacard datacard)
        {
            _datacard = datacard;
        }

        public async Task<IHttpActionResult> GetFieldBoundaries(string datacardId)
        {
            var boundaries = await _datacard.GetFieldBoundaries(datacardId);
            return Ok(boundaries);
        }
    }
}