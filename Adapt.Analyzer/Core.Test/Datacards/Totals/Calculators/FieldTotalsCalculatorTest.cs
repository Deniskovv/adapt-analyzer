using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Totals.Calculators;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Totals.Calculators
{
    [TestFixture]
    public class FieldTotalsCalculatorTest
    {
        private List<ApplicationDataModel> _dataModels;
        private FieldTotalsCalculator _fieldTotalsCalculator;

        [SetUp]
        public void Setup()
        {
            _dataModels = new List<ApplicationDataModel>();
            _fieldTotalsCalculator = new FieldTotalsCalculator();
        }

        [Test]
        public async Task ShouldGetTotalsForEachFieldInPluginWithLoggedData()
        {
            _dataModels.Add(CreateDataModelWithFields());


            var totals = await _fieldTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual(2, totals.Length);
        }

        [Test]
        public async Task ShouldGetFieldDescriptionForField()
        {
            _dataModels.Add(CreateDataModelWithFields());

            var totals = await _fieldTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual("Bob", totals[0].Description);
        }

        [Test]
        public async Task ShouldGetOperationTotalForField()
        {
            _dataModels.Add(CreateDataModelWithFields());

            var totals = await _fieldTotalsCalculator.Calculate(_dataModels);
            Assert.AreEqual(1, totals[0].OperationTotals.Length);
        }

        private static ApplicationDataModel CreateDataModelWithFields()
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
