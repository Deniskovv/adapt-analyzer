using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Totals;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.Equipment;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using AgGateway.ADAPT.ApplicationDataModel.Representations;
using AgGateway.ADAPT.Representation.RepresentationSystem;
using AgGateway.ADAPT.Representation.RepresentationSystem.ExtensionMethods;
using AgGateway.ADAPT.Representation.UnitSystem;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Totals
{
    [TestFixture]
    public class OperationTotalsCalculatorTest
    {
        private OperationTotalsCalculator _operationTotalsCalculator;
        private Field _field;
        private ApplicationDataModel _dataModel;

        [SetUp]
        public void Setup()
        {
            _field = new Field
            {
                Id =
                {
                    ReferenceId = 34
                }
            };
            _dataModel = CreateDataModel(_field);

            _operationTotalsCalculator = new OperationTotalsCalculator();
        }

        [Test]
        public async Task ShouldGetTotalsForEachOperation()
        {
            var operationTotals = await _operationTotalsCalculator.Calculate(_field, _dataModel);
            Assert.AreEqual(3, operationTotals.Length);
        }

        [Test]
        public async Task ShouldGetOperationTypeForEachOperation()
        {
            var operationTotals = await _operationTotalsCalculator.Calculate(_field, _dataModel);
            Assert.AreEqual(OperationTypeEnum.Fertilizing, operationTotals[0].OperationType);
            Assert.AreEqual(OperationTypeEnum.Swathing, operationTotals[1].OperationType);
            Assert.AreEqual(OperationTypeEnum.Harvesting, operationTotals[2].OperationType);
        }

        [Test]
        public async Task ShouldUseOnlyLogDataForFieldForOperationTotals()
        {
            var extraLogData = new LoggedData
            {
                FieldId = 324234,
                OperationData = new List<OperationData>
                {
                    new OperationData()
                }
            };
            _dataModel.Documents.LoggedData = _dataModel.Documents.LoggedData.Concat(extraLogData);

            var operationTotals = await _operationTotalsCalculator.Calculate(_field, _dataModel);
            Assert.AreEqual(3, operationTotals.Length);
        }

        [Test]
        public async Task ShouldHaveTotalForEachNumericWorkingData()
        {
            _dataModel.Documents.LoggedData.First().OperationData.First().MaxDepth = 1;
            _dataModel.Documents.LoggedData.First().OperationData.First().GetDeviceElementUses = GetDeviceElementUses;

            var operationTotals = await _operationTotalsCalculator.Calculate(_field, _dataModel);
            Assert.AreEqual(3, operationTotals[0].RepresentationTotals.Length);
        }

        private static IEnumerable<DeviceElementUse> GetDeviceElementUses(int i)
        {
            return new List<DeviceElementUse>
            {
                new DeviceElementUse
                {
                    GetWorkingDatas = GetWorkingDatas
                }
            };
        }

        private static IEnumerable<WorkingData> GetWorkingDatas()
        {
            return new List<WorkingData>
            {
                new NumericWorkingData
                {
                    Representation = RepresentationInstanceList.vrHarvestMoisture.ToModelRepresentation(),
                    UnitOfMeasure = UnitSystemManager.GetUnitOfMeasure("prcnt"),
                    Values = new List<double> {0}
                },
                new NumericWorkingData
                {
                    Representation = RepresentationInstanceList.vrYieldMass.ToModelRepresentation(),
                    UnitOfMeasure = UnitSystemManager.GetUnitOfMeasure("kg"),
                    Values = new List<double> {123}
                },
                new NumericWorkingData
                {
                    Representation = RepresentationInstanceList.vrEngineSpeed.ToModelRepresentation(),
                    UnitOfMeasure = UnitSystemManager.GetUnitOfMeasure("RPM"),
                    Values = new List<double> {453}
                }
            };
        }

        private static ApplicationDataModel CreateDataModel(Field field)
        {
            return new ApplicationDataModel
            {
                Documents = new Documents
                {
                    LoggedData = new List<LoggedData>
                    {
                        new LoggedData
                        {
                            FieldId = field.Id.ReferenceId,
                            OperationData = new List<OperationData>
                            {
                                new OperationData {OperationType = OperationTypeEnum.Fertilizing},
                                new OperationData {OperationType = OperationTypeEnum.Swathing},
                                new OperationData {OperationType = OperationTypeEnum.Harvesting}
                            }
                        }
                    }
                }
            };
        }
    }
}
