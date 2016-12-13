using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards;
using Adapt.Analyzer.Core.Datacards.Models;
using Fakes.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards
{
    [TestFixture]
    public class DatacardsControllerTest
    {
        private DatacardFake _datacardFake;
        private DatacardsController _datacardsController;

        [SetUp]
        public void Setup()
        {
            _datacardFake = new DatacardFake();
            _datacardsController = new DatacardsController(_datacardFake);
        }

        [Test]
        public async Task ShouldGetDatacards()
        {
            var datacardModels = new[] {new DatacardModel(), new DatacardModel(), new DatacardModel()};
            _datacardFake.SetupDatacardModels(datacardModels);

            var result = (OkNegotiatedContentResult<DatacardModel[]>) await _datacardsController.GetDatacards();
            Assert.AreSame(datacardModels, result.Content);
        }
    }
}
