using System;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Extract;
using Adapt.Analyzer.Core.Datacards.Metadata;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using Fakes.AgGateway;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Metadata
{
    [TestFixture]
    public class DatacardMetadataReaderTest
    {
        private PluginFactoryFake _pluginFactoryFake;
        private ConfigFake _configFake;
        private FileSystemFake _fileSystemFake;
        private DatacardMetadataReader _datacardMetadataReader;
        private DatacardPath _datacardPath;
        private string _datacardId;

        [SetUp]
        public void Setup()
        {
            _datacardId = Guid.NewGuid().ToString();
            _configFake = new ConfigFake {DatacardsDirectory = "something"};
            _fileSystemFake = new FileSystemFake();

            _datacardPath = new DatacardPath(_configFake);
            var datacardExtractor = new DatacardExtractor(_datacardPath, _fileSystemFake);

            _pluginFactoryFake = new PluginFactoryFake();
            _datacardMetadataReader = new DatacardMetadataReader(datacardExtractor, _pluginFactoryFake);
        }

        [Test]
        public async Task ShouldGetMetadataForSupprtedPlugin()
        {
            var plugin = _pluginFactoryFake.AddSupportedPlugin();
            plugin.DataModels.Add(new ApplicationDataModel());

            var metadata = await _datacardMetadataReader.Read(_datacardId);
            Assert.AreEqual(1, metadata.DataModels.Length);
        }

        [Test]
        public async Task ShouldGetMetadataForAllSupportedPlugins()
        {
            var firstPlugin = _pluginFactoryFake.AddSupportedPlugin();
            firstPlugin.DataModels.Add(new ApplicationDataModel());

            var secondPlugin = _pluginFactoryFake.AddSupportedPlugin();
            secondPlugin.DataModels.Add(new ApplicationDataModel());

            var thirdPlugin = _pluginFactoryFake.AddSupportedPlugin();
            thirdPlugin.DataModels.Add(new ApplicationDataModel());

            var metadata = await _datacardMetadataReader.Read(_datacardId);
            Assert.AreEqual(3, metadata.DataModels.Length);
        }

        [Test]
        public async Task ShouldIncludePluginNameAndVersionWithDataModel()
        {
            var firstPlugin = _pluginFactoryFake.AddSupportedPlugin("supported 1", "342");
            firstPlugin.DataModels.Add(new ApplicationDataModel());

            var metadata = await _datacardMetadataReader.Read(_datacardId);
            Assert.AreEqual("supported 1", metadata.DataModels[0].PluginName);
            Assert.AreEqual("342", metadata.DataModels[0].PluginVersion);
        }
    }
}
