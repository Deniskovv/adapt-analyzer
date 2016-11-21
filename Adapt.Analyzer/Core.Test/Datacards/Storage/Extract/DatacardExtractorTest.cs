using System;
using System.IO;
using Adapt.Analyzer.Core.Datacards.Storage;
using Adapt.Analyzer.Core.Datacards.Storage.Extract;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Storage.Extract
{
    [TestFixture]
    public class DatacardExtractorTest
    {
        private const string DatacardsDirectory = "/my/new/dir";
        private ConfigFake _configFake;
        private FileSystemFake _fileSystemFake;
        private DatacardExtractor _datacardExtractor;

        [SetUp]
        public void Setup()
        {
            _configFake = new ConfigFake();
            _configFake.SetSetting("datacards-dir", DatacardsDirectory);

            _fileSystemFake = new FileSystemFake();
            var datacardPath = new DatacardPath(_configFake);
            _datacardExtractor = new DatacardExtractor(datacardPath, _fileSystemFake);
        }

        [Test]
        public void ShouldExtractDatacardToDatacardDirectory()
        {
            var id = Guid.NewGuid().ToString();

            var datacardPath = _datacardExtractor.Extract(id);
            Assert.AreEqual(Path.Combine(DatacardsDirectory, id), datacardPath);
            Assert.AreEqual(Path.Combine(DatacardsDirectory, id + ".zip"), _fileSystemFake.ZipFilePath);
        }

        [Test]
        public void ShouldNotExtractDatacardIfAlreadyExtracted()
        {
            _fileSystemFake.DoesDirectoryExist = true;

            var id = Guid.NewGuid().ToString();

            var datacardPath = _datacardExtractor.Extract(id);
            Assert.Null(_fileSystemFake.ZipFilePath);
            Assert.AreEqual(Path.Combine(DatacardsDirectory, id), datacardPath);
        }
    }
}
