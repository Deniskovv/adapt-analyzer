using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards.Totals;
using Adapt.Analyzer.Core.Datacards.Totals.Models;
using Fakes.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards.Totals
{
    [TestFixture]
    public class TotalsControllerTest
    {
        private TotalsController _totalsController;
        private DatacardFake _datacardFake;
        private DatacardFactoryFake _datacardFactoryFake;


        [SetUp]
        public void Setup()
        {
            _datacardFake = new DatacardFake(Guid.NewGuid().ToString());
            _datacardFactoryFake = new DatacardFactoryFake(_datacardFake);

            _totalsController = new TotalsController(_datacardFactoryFake);
        }

        [Test]
        public async Task ShouldGetPluginsForDatacard()
        {
            var datacardTotal = new DatacardTotals(new PluginTotals[0]);
            _datacardFake.SetupTotals(datacardTotal);

            var result = (OkNegotiatedContentResult<DatacardTotals>)await _totalsController.GetTotals(_datacardFake.Id);
            Assert.AreSame(datacardTotal, result.Content);
        }
    }
}
