using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Totals
{
    [RoutePrefix("datacards")]
    public class TotalsController : ApiController
    {
        private readonly IDatacard _datacard;

        public TotalsController()
            : this(new Datacard())
        {
            
        }

        public TotalsController(IDatacard datacard)
        {
            _datacard = datacard;
        }

        [HttpGet]
        [Route("{datacardId}/totals")]
        public async Task<IHttpActionResult> GetTotals(string datacardId)
        {
            var totals = await _datacard.CalculateTotals(datacardId);
            return Ok(totals);
        }
    }
}