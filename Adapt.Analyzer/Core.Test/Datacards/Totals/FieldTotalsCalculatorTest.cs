using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Totals;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using Fakes.AgGateway;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Totals
{
    [TestFixture]
    public class FieldTotalsCalculatorTest
    {
        private string _datacardPath;
        private PredicatePlugin _plugin;
        private FieldTotalsCalculator _fieldTotalsCalculator;

        [SetUp]
        public void Setup()
        {
            _datacardPath = Guid.NewGuid().ToString();
            _plugin = new PredicatePlugin((s, v) => true);

            _fieldTotalsCalculator = new FieldTotalsCalculator();
        }

        [Test]
        public async Task ShouldGetTotalsForEachFieldInPluginWithLoggedData()
        {
            _plugin.DataModels.Add(CreateDataModelWithFields());


            var totals = await _fieldTotalsCalculator.Calculate(_plugin, _datacardPath);
            Assert.AreEqual(2, totals.Length);
        }

        [Test]
        public async Task ShouldGetFieldDescriptionForField()
        {
            _plugin.DataModels.Add(CreateDataModelWithFields());

            var totals = await _fieldTotalsCalculator.Calculate(_plugin, _datacardPath);
            Assert.AreEqual("Bob", totals[0].Description);
        }

        [Test]
        public async Task ShouldGetOperationTotalForField()
        {
            _plugin.DataModels.Add(CreateDataModelWithFields());

            var totals = await _fieldTotalsCalculator.Calculate(_plugin, _datacardPath);
            Assert.AreEqual(1, totals[0].OperationTotals.Length);
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
                            Description = "Bob",
                           Id =
                           {
                               ReferenceId = 854
                           }
                        }, 
                        new Field
                        {
                            Id =
                            {
                                ReferenceId = 564
                            }
                        },
                        new Field
                        {
                            Id =
                            {
                                ReferenceId = 423
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
                            FieldId = 854,
                            OperationData = new List<OperationData>
                            {
                                new OperationData()
                            }
                        }, 
                        new LoggedData
                        {
                            FieldId = 564,
                            OperationData = new List<OperationData>
                            {
                                new OperationData()
                            }
                        }
                    }
                }
            };
        }
    }
}
