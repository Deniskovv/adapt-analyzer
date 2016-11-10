using System;
using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Extract;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using Fakes.AgGateway;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards
{
    [TestFixture]
    public class DatacardTest
    {
        private const string DataCardsDirectory = "this is a directory";
        private ConfigFake _configFake;
        private FileSystemFake _fileSystemFake;
        private PluginFactoryFake _pluginFactory;
        private string _id;
        private Datacard _datacard;

        [SetUp]
        public void Setup()
        {
            _id = Guid.NewGuid().ToString();

            _configFake = new ConfigFake();
            _configFake.SetSetting("datacards-dir", DataCardsDirectory);

            _fileSystemFake = new FileSystemFake();
            _pluginFactory = new PluginFactoryFake();

            var datacardExtractor = new DatacardExtractor(new DatacardPath(_configFake), _fileSystemFake);
            _datacard = new Datacard(_id, datacardExtractor, _pluginFactory);
        }

        [Test]
        public async Task ShouldGetPluginsForDatacard()
        {
            AddSupportedPlugin("GodStuff", "3.4.1", Path.Combine(DataCardsDirectory, _id));
            AddSupportedPlugin("NotWorking", "9.1.3", Path.Combine(DataCardsDirectory, Guid.NewGuid().ToString()));
            AddSupportedPlugin("BadStuff", "2.5.7", Path.Combine(DataCardsDirectory, _id));

            var plugins = await _datacard.GetPlugins();
            Assert.AreEqual(2, plugins.Length);
        }

        [Test]
        public async Task ShouldGetMetadata()
        {
            var plugin = AddSupportedPlugin("something", "3.34", Path.Combine(DataCardsDirectory, _id));
            plugin.DataModels.Add(new ApplicationDataModel());
            plugin.DataModels.Add(new ApplicationDataModel());
            plugin.DataModels.Add(new ApplicationDataModel());

            var metadata = await _datacard.GetMetadata();
            Assert.AreEqual(3, metadata.DataModels.Length);
        }

        private PredicatePlugin AddSupportedPlugin(string name, string version, string datacardPath)
        {
            var plugin = new PredicatePlugin((s, p) => s == datacardPath)
            {
                Name = name,
                Version = version
            };
            _pluginFactory.Plugins.Add(plugin);
            return plugin;
        }
    }
}
