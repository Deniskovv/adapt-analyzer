using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Extract;
using Adapt.Analyzer.Core.Datacards.Totals;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using Fakes.AgGateway;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Totals
{
    [TestFixture]
    public class DatacardTotalsCalculatorTest
    {
        private string _datacardId;
        private PluginFactoryFake _pluginFactoryFake;
        private FileSystemFake _fileSystemFake;
        private ConfigFake _configFake;
        private DatacardPath _datacardPath;
        private DatacardTotalsCalculator _datacardTotalsCalculator;

        [SetUp]
        public void Setup()
        {
            _datacardId = Guid.NewGuid().ToString();
            _configFake = new ConfigFake {DatacardsDirectory = "something"};
            _fileSystemFake = new FileSystemFake();
            _datacardPath = new DatacardPath(_configFake);

            var datacardExtractor = new DatacardExtractor(_datacardPath, _fileSystemFake);

            _pluginFactoryFake = new PluginFactoryFake();
            
            _datacardTotalsCalculator = new DatacardTotalsCalculator(datacardExtractor, _pluginFactoryFake, new FieldTotalsCalculator());
        }

        [Test]
        public async Task ShouldCalculateTotalsForEachSupportedPlugin()
        {
            _pluginFactoryFake.AddSupportedPlugin("One", "3.4");
            _pluginFactoryFake.AddSupportedPlugin("Two", "3.3");
            _pluginFactoryFake.AddUnsupportedPlugin("Three", "1.2");

            var totals = await _datacardTotalsCalculator.Calculate(_datacardId);
            Assert.AreEqual(2, totals.PluginTotals.Length);
        }

        [Test]
        public async Task ShouldGetPluginNameForEachSupportedPlugin()
        {
            _pluginFactoryFake.AddSupportedPlugin("One", "3.4");

            var totals = await _datacardTotalsCalculator.Calculate(_datacardId);
            Assert.AreEqual("One", totals.PluginTotals[0].PluginName);
        }

        [Test]
        public async Task ShouldGetPluginVersionForEachSupportedPlugin()
        {
            _pluginFactoryFake.AddSupportedPlugin("Three", "3.5.7.1");

            var totals = await _datacardTotalsCalculator.Calculate(_datacardId);
            Assert.AreEqual("3.5.7.1", totals.PluginTotals[0].PluginVersion);
        }

        [Test]
        public async Task ShouldGetTotalsForFieldsInPlugin()
        {
            var plugin = _pluginFactoryFake.AddSupportedPlugin();
            plugin.DataModels.Add(CreateDataModelWithFields());

            var totals = await _datacardTotalsCalculator.Calculate(_datacardId);
            Assert.AreEqual(1, totals.PluginTotals[0].FieldTotals.Length);
        }

        private ApplicationDataModel CreateDataModelWithFields()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        new Field
                        {
                           Id =
                           {
                               ReferenceId = 854
                           }
                        }
                    }
                },
                Documents = new Documents
                {
                    LoggedData = new List<LoggedData>
                    {
                        new LoggedData
                        {
                            FieldId = 854
                        }
                    }
                }
            };
        }
    }
}
