using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analzyer.Api.Upload
{
    [RoutePrefix("upload")]
    public class UploadController : ApiController
    {
        private readonly IFile _file;
        private readonly string _datacardsDirectory;

        public UploadController(IConfig config, IFile file)
        {
            _file = file;
            _datacardsDirectory = config.GetSetting("datacards-dir");
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            var bytes = await Request.Content.ReadAsByteArrayAsync();
            var id = Guid.NewGuid();
            var datacardPath = Path.Combine(_datacardsDirectory, id + ".zip");
            _file.WriteAllBytes(datacardPath, bytes);
            return Ok(id.ToString());
        }
    }
}