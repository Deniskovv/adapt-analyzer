using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Metadata;
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
        private string _datacardId;

        [SetUp]
        public void Setup()
        {
            _datacardId = Guid.NewGuid().ToString();
            _datacardFake = new DatacardFake();
            _datacardFactoryFake = new DatacardFactoryFake(_datacardFake);
            _metadataController = new MetadataController(_datacardFactoryFake);
        }

        [Test]
        public async Task ShouldGetMetadataForDatacard()
        {
            var metadata = new Core.Datacards.Metadata.Metadata(new PluginDataModel[0]);
            _datacardFake.SetupMetadata(metadata);

            var result = (OkNegotiatedContentResult<Core.Datacards.Metadata.Metadata>)await _metadataController.GetMetadata(_datacardId);
            Assert.AreSame(metadata, result.Content);
        }
    }
}
