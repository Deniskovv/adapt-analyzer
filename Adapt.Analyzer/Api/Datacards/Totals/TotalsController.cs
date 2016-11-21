using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Totals
{
    [RoutePrefix("datacards")]
    public class TotalsController : ApiController
    {
        private readonly IDatacardFactory _datacardFactory;

        public TotalsController()
            : this(new DatacardFactory())
        {
            
        }

        public TotalsController(IDatacardFactory datacardFactory)
        {
            _datacardFactory = datacardFactory;
        }

        [HttpGet]
        [Route("{datacardId}/totals")]
        public async Task<IHttpActionResult> GetTotals(string datacardId)
        {
            var datacard = _datacardFactory.Create();
            var totals = await datacard.CalculateTotals(datacardId);
            return Ok(totals);
        }
    }
}