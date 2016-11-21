using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Storage;
using Adapt.Analyzer.Core.Datacards.Storage.Extract;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using Fakes.AgGateway;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Metadata
{
    [TestFixture]
    public class DatacardMetadataReaderTest
    {
        private DatacardMetadataReader _datacardMetadataReader;
        private List<StorageDataModel> _dataModels;

        [SetUp]
        public void Setup()
        {
            _dataModels = new List<StorageDataModel>();
            _datacardMetadataReader = new DatacardMetadataReader();
        }

        [Test]
        public async Task ShouldGetMetadataForSupprtedPlugin()
        {
            AddStorageModel();

            var metadata = await _datacardMetadataReader.Read(_dataModels);
            Assert.AreEqual(1, metadata.DataModels.Length);
        }

        [Test]
        public async Task ShouldGetMetadataForAllSupportedPlugins()
        {
            AddStorageModel();
            AddStorageModel();
            AddStorageModel();

            var metadata = await _datacardMetadataReader.Read(_dataModels);
            Assert.AreEqual(3, metadata.DataModels.Length);
        }

        [Test]
        public async Task ShouldIncludePluginNameAndVersionWithDataModel()
        {
            AddStorageModel("supported 1", "342");

            var metadata = await _datacardMetadataReader.Read(_dataModels);
            Assert.AreEqual("supported 1", metadata.DataModels[0].PluginName);
            Assert.AreEqual("342", metadata.DataModels[0].PluginVersion);
        }

        private void AddStorageModel(string pluginName = null, string pluginVersion = null)
        {
            var plugin = new PredicatePlugin
            {
                Name = pluginName,
                Version = pluginVersion,
                DataModels =
                {
                    new ApplicationDataModel()
                }
            };
            var storageModel = new StorageDataModel(null, plugin);
            _dataModels.Add(storageModel);
        }
    }
}
