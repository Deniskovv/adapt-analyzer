using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.Test.Adapt.Helpers;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;
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

        [Test]
        public async Task ShouldOnlyGetFieldsWithoutActiveBoundaries()
        {
            AddDataModel(CreateDataModelWithFieldsWithoutActiveBoundaries());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(3, fieldBoundaries.Length);
        }

        [Test]
        public async Task ShouldOnlyGetFieldsWithBoundaries()
        {
            AddDataModel(CreateDataModelWithFieldsWithoutBoundaries());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(1, fieldBoundaries.Length);
        }

        [Test]
        public async Task ShouldGetEachBoundaryForEachField()
        {
            AddDataModel(CreateDataModelWithFieldWithMultipleBoundaries());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(3, fieldBoundaries[0].Boundaries.Length);
            Assert.AreEqual(2, fieldBoundaries[1].Boundaries.Length);
        }

        [Test]
        public async Task ShouldGetFieldDescriptionForEachField()
        {
            AddDataModel(CreateDataModelWithOneFieldBoundary());
            _dataModels[0].DataModels[0].Catalog.Fields[0].Description = "Something good";

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual("Something good", fieldBoundaries[0].Description);
        }

        [Test]
        public async Task ShouldGetFieldReferenceIdForEachField()
        {
            AddDataModel(CreateDataModelWithOneFieldBoundary());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(_dataModels[0].DataModels[0].Catalog.Fields[0].Id.ReferenceId, fieldBoundaries[0].Id);
        }

        [Test]
        public async Task ShouldGetCenterPointForField()
        {
            AddDataModel(CreateDataModelWithOneFieldBoundary());

            var fieldBoundaries = await _fieldBoundaryReader.GetFieldBoundaries(_dataModels);
            Assert.AreEqual(60, fieldBoundaries[0].CenterPoint.Longitude);
            Assert.AreEqual(34.5, fieldBoundaries[0].CenterPoint.Latitude);
        }

        private void AddDataModel(ApplicationDataModel dataModel)
        {
            var plugin = new PredicatePlugin
            {
                DataModels = { dataModel }
            };
            _dataModels.Add(new StorageDataModel(null, plugin));
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

        private static ApplicationDataModel CreateDataModelWithFieldsWithoutActiveBoundaries()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        CreateField(2, null),
                        CreateField(3, null),
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

        private static ApplicationDataModel CreateDataModelWithFieldsWithoutBoundaries()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        CreateField(2, null),
                        CreateField(3, null),
                        CreateField(1, 98)
                    },
                    FieldBoundaries = new List<FieldBoundary>
                    {
                        CreateBoundary(98, 1)
                    }
                }
            };
        }

        private static ApplicationDataModel CreateDataModelWithFieldWithMultipleBoundaries()
        {
            return new ApplicationDataModel
            {
                Catalog = new Catalog
                {
                    Fields = new List<Field>
                    {
                        CreateField(1, 98),
                        CreateField(3, 98)
                    },
                    FieldBoundaries = new List<FieldBoundary>
                    {
                        CreateBoundary(45, 1),
                        CreateBoundary(47, 1),
                        CreateBoundary(98, 1),
                        CreateBoundary(78, 3),
                        CreateBoundary(72, 3)
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
                },
                SpatialData = new MultiPolygon
                {
                    Polygons = new List<Polygon>
                    {
                        new Polygon
                        {
                            ExteriorRing = new LinearRing
                            {
                                Points = new List<Point>
                                {
                                    PointFactory.CreatePoint(56, 21),
                                    PointFactory.CreatePoint(58, 67),
                                    PointFactory.CreatePoint(54, 32),
                                }
                            }
                        },
                        new Polygon
                        {
                            ExteriorRing = new LinearRing
                            {
                                Points = new List<Point>
                                {
                                    PointFactory.CreatePoint(61, 3),
                                    PointFactory.CreatePoint(66, 7),
                                    PointFactory.CreatePoint(62, 2)
                                }
                            }
                        }
                    }
                }
            };
        }

        private static Field CreateField(int fieldId, int? activeBoundaryId)
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
    }
}
