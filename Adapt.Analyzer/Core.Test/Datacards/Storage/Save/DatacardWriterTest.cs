using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Storage;
using Adapt.Analyzer.Core.Datacards.Storage.Save;
using Adapt.Analyzer.Core.General;
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
            _configFake = new ConfigFake { DatacardsDirectory = DataCardsDirectory };
            _datacardWriter = new DatacardWriter(new DatacardPath(_configFake), _fileSystemFake, new Serializer());
        }

        [Test]
        public async Task UploadShouldSaveFileToDatacardsDirectory()
        {
            var bytes = new byte[] { 34, 23, 7, 6, 8, 23 };
            var newDatacard = new DatacardModel(name: "One", bytes: bytes);
        
            var result = await _datacardWriter.Write(newDatacard);
            Assert.Contains(Path.Combine(DataCardsDirectory, result, "Datacard.zip"), _fileSystemFake.WrittenFiles);
            Assert.AreEqual(bytes, _fileSystemFake.WrittenBytes);
        }

        [Test]
        public async Task UploadShouldCreateDirectoryForDatacard()
        {
            _fileSystemFake.DoesDirectoryExist = false;
            var bytes = new byte[] { };
            var newDatacard = new DatacardModel(name: "Two", bytes: bytes);

            var id = await _datacardWriter.Write(newDatacard);
            Assert.Contains(Path.Combine(DataCardsDirectory, id), _fileSystemFake.CreatedDirectories);
        }

        [Test]
        public async Task UploadShouldCreateDatacardsDirectory()
        {
            _fileSystemFake.DoesDirectoryExist = false;
            var bytes = new byte[] {};
            var newDatacard = new DatacardModel(name: "Three", bytes: bytes);

            await _datacardWriter.Write(newDatacard);
            Assert.Contains(DataCardsDirectory, _fileSystemFake.CreatedDirectories);
        }

        [Test]
        public async Task UploadShouldNotCreateDatacardsDirectory()
        {
            _fileSystemFake.DoesDirectoryExist = true;
            var bytes = new byte[] { };
            var newDatacard = new DatacardModel(name: "Four", bytes: bytes);

            await _datacardWriter.Write(newDatacard);
            Assert.IsEmpty(_fileSystemFake.CreatedDirectories);
        }
    }
}
