using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards.Metadata;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using Fakes.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards.Metadata
{
    [TestFixture]
    public class MetadataControllerTest
    {
        private MetadataController _metadataController;
        private DatacardFake _datacardFake;
        private DatacardFactoryFake _datacardFactoryFake;

        [SetUp]
        public void Setup()
        {
            _datacardFake = new DatacardFake(Guid.NewGuid().ToString());
            _datacardFactoryFake = new DatacardFactoryFake(_datacardFake);
            _metadataController = new MetadataController(_datacardFactoryFake);
        }

        [Test]
        public async Task ShouldGetMetadataForDatacard()
        {
            var metadata = new Core.Datacards.Metadata.Metadata(new ApplicationDataModel[0]);
            _datacardFake.SetupMetadata(metadata);

            var result = (OkNegotiatedContentResult<Core.Datacards.Metadata.Metadata>)await _metadataController.GetMetadata(_datacardFake.Id);
            Assert.AreSame(metadata, result.Content);
        }
    }
}
