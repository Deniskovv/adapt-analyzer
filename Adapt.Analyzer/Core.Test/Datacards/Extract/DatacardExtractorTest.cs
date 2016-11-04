using System;
using System.IO;
using Adapt.Analyzer.Core.Datacards.Extract;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Extract
{
    [TestFixture]
    public class DatacardExtractorTest
    {
        private const string DatacardsDirectory = "/my/new/dir";
        private ConfigFake _configFake;
        private FileFake _fileFake;
        private DatacardExtractor _datacardExtractor;

        [SetUp]
        public void Setup()
        {
            _configFake = new ConfigFake();
            _configFake.SetSetting("datacards-dir", DatacardsDirectory);

            _fileFake = new FileFake();
            _datacardExtractor = new DatacardExtractor(_configFake, _fileFake);
        }

        [Test]
        public void ShouldExtractDatacardToDatacardDirectory()
        {
            var id = Guid.NewGuid().ToString();

            var datacardPath = _datacardExtractor.Extract(id);
            Assert.AreEqual(Path.Combine(DatacardsDirectory, id), datacardPath);
            Assert.AreEqual(Path.Combine(DatacardsDirectory, id + ".zip"), _fileFake.ZipFilePath);
        }
    }
}
