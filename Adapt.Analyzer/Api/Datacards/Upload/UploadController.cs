using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards.Save;

namespace Adapt.Analyzer.Api.Datacards.Upload
{
    [RoutePrefix("datacards/upload")]
    public class UploadController : ApiController
    {
        private readonly IDatacardWriter _datacardWriter;

        public UploadController()
            : this(new DatacardWriter())
        {
            
        }

        public UploadController(IDatacardWriter datacardWriter)
        {
            _datacardWriter = datacardWriter;
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            var bytes = await Request.Content.ReadAsByteArrayAsync();
            var id = await _datacardWriter.Write(bytes);
            return Ok(id);
        }
    }
}