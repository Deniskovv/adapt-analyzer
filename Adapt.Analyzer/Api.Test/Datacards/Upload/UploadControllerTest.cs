using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analzyer.Api.Datacards.Upload;
using Fakes.Datacards.Save;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards.Upload
{
    [TestFixture]
    public class UploadControllerTest
    {
        private const string DataCardsDirectory = "something goes here";
        private UploadController _uploadController;
        private DatacardWriterFake _datacardWriterFake;

        [SetUp]
        public void Setup()
        {
            _datacardWriterFake = new DatacardWriterFake();
            _uploadController = new UploadController(_datacardWriterFake);
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

            Assert.AreEqual(_datacardWriterFake.WrittenBytes, bytes);
            Assert.AreEqual(_datacardWriterFake.Id, result.Content);
        }
    }
}
