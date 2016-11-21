using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.Datacards.Totals.Calculators;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using Fakes.AgGateway;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Totals.Calculators
{
    [TestFixture]
    public class DatacardTotalsCalculatorTest
    {
        private List<StorageDataModel> _dataModels;
        private DatacardTotalsCalculator _datacardTotalsCalculator;

        [SetUp]
        public void Setup()
        {
            _dataModels = new List<StorageDataModel>();
            _datacardTotalsCalculator = new DatacardTotalsCalculator(new FieldTotalsCalculator());
        }

        [Test]
        public async Task ShouldCalculateTotalsForEachSupportedPlugin()
        {
            AddDataModel("One", "3.4");
            AddDataModel("Two", "3.3");

            var totals = await _datacardTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual(2, totals.PluginTotals.Length);
        }

        [Test]
        public async Task ShouldGetPluginNameForEachSupportedPlugin()
        {
            AddDataModel("One", "3.4");

            var totals = await _datacardTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual("One", totals.PluginTotals[0].PluginName);
        }

        [Test]
        public async Task ShouldGetPluginVersionForEachSupportedPlugin()
        {
            AddDataModel("Three", "3.5.7.1");

            var totals = await _datacardTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual("3.5.7.1", totals.PluginTotals[0].PluginVersion);
        }

        [Test]
        public async Task ShouldGetTotalsForFieldsInPlugin()
        {
            AddDataModel(dataModel: CreateDataModelWithFields());

            var totals = await _datacardTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual(1, totals.PluginTotals[0].FieldTotals.Length);
        }

        private void AddDataModel(string pluginName = null, string pluginVersion = null, ApplicationDataModel dataModel = null)
        {
            var plugin = new PredicatePlugin
            {
                Name = pluginName,
                Version = pluginVersion,
                DataModels =
                {
                    dataModel ?? new ApplicationDataModel()
                }
            };
            var storageModel = new StorageDataModel(null, plugin);
            _dataModels.Add(storageModel);
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
