using System;
using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Models;

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
        public async Task<IHttpActionResult> Upload([FromBody] UploadedDatacard uploadedDatacard)
        {
            var bytes = Convert.FromBase64String(uploadedDatacard.File);
            var newDatacard = new DatacardModel(name: uploadedDatacard.Name, bytes: bytes);
            var id = await _datacard.Save(newDatacard);
            return Ok(id);
        }
    }
}