using System;
using Adapt.Analyzer.Core.Datacards;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards
{
    [TestFixture]
    public class DatacardFactoryTest
    {
        private DatacardFactory _datacardFactory;

        [SetUp]
        public void Setup()
        {
            _datacardFactory = new DatacardFactory();
        }

        [Test]
        public void ShouldCreateDatacard()
        {
            var id = Guid.NewGuid().ToString();
            var datacard = _datacardFactory.Create(id);
            Assert.AreEqual(id, datacard.Id);
            Assert.IsInstanceOf<Datacard>(datacard);
        }
    }
}
