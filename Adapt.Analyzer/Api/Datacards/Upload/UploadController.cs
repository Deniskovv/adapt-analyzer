using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;

namespace Adapt.Analyzer.Api.Datacards.Upload
{
    [RoutePrefix("datacards/upload")]
    public class UploadController : ApiController
    {
        private readonly IDatacard _datacard;

        public UploadController()
            : this(new Datacard())
        {
            
        }

        public UploadController(IDatacard datacard)
        {
            _datacard = datacard;
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            var bytes = await Request.Content.ReadAsByteArrayAsync();
            var id = await _datacard.Save(bytes);
            return Ok(id);
        }
    }
}