using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards.Upload;
using Fakes.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards.Upload
{
    [TestFixture]
    public class UploadControllerTest
    {
        private UploadController _uploadController;
        private DatacardFake _datacardFake;

        [SetUp]
        public void Setup()
        {
            _datacardFake = new DatacardFake();
            _uploadController = new UploadController(_datacardFake);
        }

        [Test]
        public async Task UploadShouldSaveFileToDatacardsDirectory()
        {
            var bytes = new byte[] { 34, 23, 7, 6, 8, 23 };
            var uploadedDatacard = new UploadedDatacard
            {
                File = Convert.ToBase64String(bytes),
                Name = "This"
            };

            var result = (OkNegotiatedContentResult<string>) await _uploadController.Upload(uploadedDatacard);

            Assert.AreEqual(_datacardFake.WrittenBytes, bytes);
            Assert.AreEqual(_datacardFake.NewId, result.Content);
        }
    }
}
