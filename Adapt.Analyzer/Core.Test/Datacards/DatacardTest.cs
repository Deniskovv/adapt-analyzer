using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Extract;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Plugins;
using Adapt.Analyzer.Core.Datacards.Totals;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using Fakes.AgGateway;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards
{
    [TestFixture]
    public class DatacardTest
    {
        private ConfigFake _configFake;
        private FileSystemFake _fileSystemFake;
        private PluginFactoryFake _pluginFactory;
        private string _id;
        private Datacard _datacard;
        private DatacardPath _datacardPath;

        [SetUp]
        public void Setup()
        {
            _id = Guid.NewGuid().ToString();

            _configFake = new ConfigFake {DatacardsDirectory = "this is a directory"};

            _fileSystemFake = new FileSystemFake();
            _pluginFactory = new PluginFactoryFake();

            _datacardPath = new DatacardPath(_configFake);
            var datacardExtractor = new DatacardExtractor(_datacardPath, _fileSystemFake);
            var datacardPluginFinder = new DatacardPluginFinder(datacardExtractor, _pluginFactory);
            var datacardMetadataReader = new DatacardMetadataReader(datacardExtractor, _pluginFactory);
            var datacardTotalsCalculator = new DatacardTotalsCalculator(datacardExtractor, _pluginFactory, new FieldTotalsCalculator());
            _datacard = new Datacard(_id, datacardPluginFinder, datacardMetadataReader, datacardTotalsCalculator);
        }

        [Test]
        public async Task ShouldGetPluginsForDatacard()
        {
            _pluginFactory.AddSupportedPlugin("GodStuff");
            _pluginFactory.AddUnsupportedPlugin("NotWorking");
            _pluginFactory.AddSupportedPlugin("BadStuff");

            var plugins = await _datacard.GetPlugins();
            Assert.AreEqual(2, plugins.Length);
        }

        [Test]
        public async Task ShouldGetMetadata()
        {
            var plugin = _pluginFactory.AddSupportedPlugin();
            plugin.DataModels.Add(new ApplicationDataModel());
            plugin.DataModels.Add(new ApplicationDataModel());
            plugin.DataModels.Add(new ApplicationDataModel());

            var metadata = await _datacard.GetMetadata();
            Assert.AreEqual(3, metadata.DataModels.Length);
        }

        [Test]
        public async Task ShouldCalculateTotalsForDatacard()
        {
            var plugin = _pluginFactory.AddSupportedPlugin();
            plugin.DataModels.Add(CreateDataModelWithTotals());

            var totals = await _datacard.CalculateTotals();
            Assert.AreEqual(1, totals.PluginTotals.Length);
        }

        private ApplicationDataModel CreateDataModelWithTotals()
        {
            return new ApplicationDataModel
            {
                Documents = new Documents
                {
                    LoggedData = new List<LoggedData>
                    {
                        new LoggedData
                        {
                            OperationData = new List<OperationData>
                            {
                                new OperationData
                                {
                                    OperationType = OperationTypeEnum.Harvesting
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
