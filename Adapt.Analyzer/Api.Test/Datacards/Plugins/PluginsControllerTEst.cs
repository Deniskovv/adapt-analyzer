using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Adapt.Analyzer.Api.Datacards.Plugins;
using Adapt.Analyzer.Core.Datacards.Plugins.Models;
using Fakes.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.Datacards.Plugins
{
    [TestFixture]
    public class PluginsControllerTest
    {
        private PluginsController _pluginsController;
        private DatacardFake _datacardFake;
        private string _datacardId;


        [SetUp]
        public void Setup()
        {
            _datacardId = Guid.NewGuid().ToString();
            _datacardFake = new DatacardFake();

            _pluginsController = new PluginsController(_datacardFake);
        }

        [Test]
        public async Task ShouldGetPluginsForDatacard()
        {
            var plugins = new[] {new Plugin(), new Plugin()};
            _datacardFake.SetupPlugins(plugins);

            var result = (OkNegotiatedContentResult<Plugin[]>) await _pluginsController.GetPlugins(_datacardId);
            Assert.AreSame(plugins, result.Content);
        }
    }
}
