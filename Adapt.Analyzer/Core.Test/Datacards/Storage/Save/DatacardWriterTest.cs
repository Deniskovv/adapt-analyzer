using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Storage.Save;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Storage.Save
{
    [TestFixture]
    public class DatacardWriterTest
    {
        private const string DataCardsDirectory = "something goes here";
        private DatacardWriter _datacardWriter;
        private FileSystemFake _fileSystemFake;
        private ConfigFake _configFake;

        [SetUp]
        public void Setup()
        {
            _fileSystemFake = new FileSystemFake();
            _configFake = new ConfigFake();
            _configFake.SetSetting("datacards-dir", DataCardsDirectory);
            _datacardWriter = new DatacardWriter(_configFake, _fileSystemFake);
        }

        [Test]
        public async Task UploadShouldSaveFileToDatacardsDirectory()
        {
            var bytes = new byte[] { 34, 23, 7, 6, 8, 23 };
        
            var result = await _datacardWriter.Write(bytes);
            Assert.AreEqual(_fileSystemFake.WrittenFile, Path.Combine(DataCardsDirectory, result + ".zip"));
            Assert.AreEqual(_fileSystemFake.WrittenBytes, bytes);
        }
    }
}
