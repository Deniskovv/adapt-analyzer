using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards.Boundaries;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Fakes.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards.Boundaries
{
    [TestFixture]
    public class BoundariesControllerTest
    {
        private DatacardFake _datacardFake;
        private BoundariesController _boundariesController;

        [SetUp]
        public void Setup()
        {
            _datacardFake = new DatacardFake();
            _boundariesController = new BoundariesController(_datacardFake);
        }

        [Test]
        public async Task ShouldGetFieldBoundaries()
        {
            var fieldBoundaries = new []{new FieldBoundary(1, null, null), new FieldBoundary(2, null, null), new FieldBoundary(3, null, null)};
            _datacardFake.SetupFieldBoundaries(fieldBoundaries);

            var result = (OkNegotiatedContentResult<FieldBoundary[]>)await _boundariesController.GetFieldBoundaries(Guid.NewGuid().ToString());
            Assert.AreSame(fieldBoundaries, result.Content);
        }
    }
}
