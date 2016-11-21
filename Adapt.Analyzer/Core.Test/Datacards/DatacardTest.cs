using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Boundaries;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Plugins;
using Adapt.Analyzer.Core.Datacards.Storage;
using Adapt.Analyzer.Core.Datacards.Storage.Extract;
using Adapt.Analyzer.Core.Datacards.Totals.Calculators;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
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
            var datacardStorage = new DatacardStorage(null, datacardExtractor, _pluginFactory);
            var datacardPluginFinder = new DatacardPluginFinder();
            var datacardMetadataReader = new DatacardMetadataReader();
            var datacardTotalsCalculator = new DatacardTotalsCalculator(new FieldTotalsCalculator());
            _datacard = new Datacard(datacardStorage, datacardPluginFinder, datacardMetadataReader, datacardTotalsCalculator, new FieldBoundaryReader());
        }

        [Test]
        public async Task ShouldGetPluginsForDatacard()
        {
            _pluginFactory.AddSupportedPlugin("GodStuff");
            _pluginFactory.AddUnsupportedPlugin("NotWorking");
            _pluginFactory.AddSupportedPlugin("BadStuff");

            var plugins = await _datacard.GetPlugins(_id);
            Assert.AreEqual(2, plugins.Length);
        }

        [Test]
        public async Task ShouldGetMetadata()
        {
            var plugin = _pluginFactory.AddSupportedPlugin();
            plugin.DataModels.Add(new ApplicationDataModel());
            plugin.DataModels.Add(new ApplicationDataModel());
            plugin.DataModels.Add(new ApplicationDataModel());

            var metadata = await _datacard.GetMetadata(_id);
            Assert.AreEqual(3, metadata.DataModels.Length);
        }

        [Test]
        public async Task ShouldCalculateTotalsForDatacard()
        {
            var plugin = _pluginFactory.AddSupportedPlugin();
            plugin.DataModels.Add(CreateDataModelWithTotals());

            var totals = await _datacard.CalculateTotals(_id);
            Assert.AreEqual(1, totals.PluginTotals.Length);
        }

        [Test]
        public async Task ShouldGetFieldBoundaries()
        {
            var plugin = _pluginFactory.AddSupportedPlugin();
            plugin.DataModels.Add(CreateDataModelWithFieldBoundaries());

            var fieldBoundaries = await _datacard.GetFieldBoundaries(_id);
            Assert.AreEqual(1, fieldBoundaries.Length);
        }

        private ApplicationDataModel CreateDataModelWithFieldBoundaries()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        new Field
                        {
                            ActiveBoundaryId = 43,
                            Id =
                            {
                                ReferenceId = 64
                            }
                        }
                    },
                    FieldBoundaries = new List<FieldBoundary>
                    {
                        new FieldBoundary
                        {
                            FieldId = 64,
                            Id =
                            {
                                ReferenceId = 34
                            }
                        }
                    }
                }
            };
        }

        private static ApplicationDataModel CreateDataModelWithTotals()
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
