using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analzyer.Api.Upload;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Upload
{
    [TestFixture]
    public class UploadControllerTest
    {
        private const string DataCardsDirectory = "something goes here";
        private UploadController _uploadController;
        private FileFake _fileFake;
        private ConfigFake _configFake;

        [SetUp]
        public void Setup()
        {
            _fileFake = new FileFake();
            _configFake = new ConfigFake();
            _configFake.SetSetting("datacards-dir", DataCardsDirectory);
            _uploadController = new UploadController(_configFake, _fileFake);
        }

        [Test]
        public async Task UploadShouldSaveFileToDatacardsDirectory()
        {
            var bytes = new byte[] {34, 23, 7, 6, 8, 23};
            _uploadController.Request = new HttpRequestMessage(HttpMethod.Post, "")
            {
                Content = new ByteArrayContent(bytes)
            };
            var result = (OkNegotiatedContentResult<string>)await _uploadController.Upload();
            Assert.AreEqual(_fileFake.WrittenFile, Path.Combine(DataCardsDirectory, result.Content + ".zip"));
            Assert.AreEqual(_fileFake.WrittenBytes, bytes);
        }
    }
}
