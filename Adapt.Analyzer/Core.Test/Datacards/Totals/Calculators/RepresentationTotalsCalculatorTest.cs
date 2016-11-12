using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Totals.Calculators;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.Representation.RepresentationSystem;
using AgGateway.ADAPT.Representation.RepresentationSystem.ExtensionMethods;
using AgGateway.ADAPT.Representation.UnitSystem;
using NUnit.Framework;
using NumericRepresentation = AgGateway.ADAPT.Representation.RepresentationSystem.NumericRepresentation;

namespace Adapt.Analyzer.Core.Test.Datacards.Totals.Calculators
{
    [TestFixture]
    public class RepresentationTotalsCalculatorTest
    {
        private List<NumericWorkingData> _workingData;
        private List<SpatialRecord> _spatialRecords;
        private RepresentationTotalsCalculator _representationTotalsCalculator;

        [SetUp]
        public void Setup()
        {
            _workingData = new List<NumericWorkingData>();
            _spatialRecords = new List<SpatialRecord>();

            _representationTotalsCalculator = new RepresentationTotalsCalculator();
        }

        [Test]
        public async Task ShouldGetOneTotalForEachRepresentation()
        {
            AddWorkingData(RepresentationInstanceList.vrYieldMass, "kg", 3);
            AddWorkingData(RepresentationInstanceList.vrHarvestMoisture, "prcnt", 23);
            AddWorkingData(RepresentationInstanceList.vrEngineSpeed, "RPM", 4500);

            var representationTotals = await _representationTotalsCalculator.Calculate(_workingData, _spatialRecords);
            Assert.AreEqual(3, representationTotals.Length);
        }

        [Test]
        public async Task ShouldSumNonRatioUnitsOfMeasure()
        {
            var workingData = AddWorkingData(RepresentationInstanceList.vrYieldMass, "kg", 6);
            AddSpatialRecord().SetNumericMeterValue(workingData, 45);
            AddSpatialRecord().SetNumericMeterValue(workingData, 67);
            AddSpatialRecord().SetNumericMeterValue(workingData, 98);

            var representationTotals = await _representationTotalsCalculator.Calculate(_workingData, _spatialRecords);
            Assert.AreEqual(210, representationTotals[0].Total);
            Assert.AreEqual("kg", representationTotals[0].UnitOfMeasure);
            Assert.AreEqual("vrYieldMass", representationTotals[0].Representation);
        }

        [Test]
        public async Task ShouldAverageRatioUnitsOfMeasure()
        {
            var workingData = AddWorkingData(RepresentationInstanceList.vrHarvestMoisture, "prcnt", 54);
            AddSpatialRecord().SetNumericMeterValue(workingData, 7.4);
            AddSpatialRecord().SetNumericMeterValue(workingData, 12.4);
            AddSpatialRecord().SetNumericMeterValue(workingData, 23.4);

            var representationTotals = await _representationTotalsCalculator.Calculate(_workingData, _spatialRecords);
            Assert.AreEqual(14.4, representationTotals[0].Total);
            Assert.AreEqual("prcnt", representationTotals[0].UnitOfMeasure);
            Assert.AreEqual("vrHarvestMoisture", representationTotals[0].Representation);
        }

        private NumericWorkingData AddWorkingData(NumericRepresentation representation, string uom, double value)
        {
            var numericWorkingData = new NumericWorkingData
            {
                Representation = representation.ToModelRepresentation(),
                UnitOfMeasure = UnitSystemManager.GetUnitOfMeasure(uom),
                Values = new List<double> {value}
            };
            _workingData.Add(numericWorkingData);
            return numericWorkingData;
        }

        private SpatialRecord AddSpatialRecord()
        {
            var record = new SpatialRecord();
            _spatialRecords.Add(record);
            return record;
        }
    }
}
