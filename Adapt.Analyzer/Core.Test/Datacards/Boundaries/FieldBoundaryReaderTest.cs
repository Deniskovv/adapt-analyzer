using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using Fakes.AgGateway;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Boundaries
{
    [TestFixture]
    public class FieldBoundaryReaderTest
    {
        private List<StorageDataModel> _dataModels;
        private FieldBoundaryReader _fieldBoundaryReader;

        [SetUp]
        public void Setup()
        {
            _dataModels = new List<StorageDataModel>();
            _fieldBoundaryReader = new FieldBoundaryReader();
        }

        [Test]
        public async Task ShouldGetFieldWithActiveBoundary()
        {
            AddDataModel(CreateDataModelWithOneFieldBoundary());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(1, fieldBoundaries.Length);
        }

        [Test]
        public async Task ShouldGetFieldsWithActiveBoundaries()
        {
            AddDataModel(CreateDataModelWithMultipleFieldBoundaries());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(3, fieldBoundaries.Length);
        }

        private static ApplicationDataModel CreateDataModelWithOneFieldBoundary()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        CreateField(54, 31)
                    },
                    FieldBoundaries = new List<FieldBoundary>
                    {
                        CreateBoundary(31, 54)
                    }
                }
            };
        }

        private static FieldBoundary CreateBoundary(int boundaryId, int fieldId)
        {
            return new FieldBoundary
            {
                FieldId = fieldId,
                Id =
                {
                    ReferenceId = boundaryId
                }
            };
        }

        private static Field CreateField(int fieldId, int activeBoundaryId)
        {
            return new Field
            {
                ActiveBoundaryId = activeBoundaryId,
                Id =
                {
                    ReferenceId = fieldId
                }
            };
        }

        private static ApplicationDataModel CreateDataModelWithMultipleFieldBoundaries()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        CreateField(2, 34),
                        CreateField(3, 65),
                        CreateField(1, 98)
                    },
                    FieldBoundaries = new List<FieldBoundary>
                    {
                        CreateBoundary(34, 2),
                        CreateBoundary(65, 3),
                        CreateBoundary(98, 1)
                    }
                }
            };
        }

        private void AddDataModel(ApplicationDataModel dataModel)
        {
            var plugin = new PredicatePlugin
            {
                DataModels = { dataModel }
            };
            _dataModels.Add(new StorageDataModel(null, plugin));
        }
    }
}
