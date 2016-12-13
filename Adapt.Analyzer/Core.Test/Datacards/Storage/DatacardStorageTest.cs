using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Storage;
using Adapt.Analyzer.Core.Datacards.Storage.Extract;
using Adapt.Analyzer.Core.Datacards.Storage.Save;
using Adapt.Analyzer.Core.General;
using Fakes.AgGateway;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Storage
{
    [TestFixture]
    public class DatacardStorageTest
    {
        private DatacardStorage _datacardStorage;
        private ConfigFake _configFake;
        private FileSystemFake _fileSystemFake;
        private Serializer _serializer;

        [SetUp]
        public void Setup()
        {
            _configFake = new ConfigFake {DatacardsDirectory = Path.GetTempPath() };
            _fileSystemFake = new FileSystemFake();
            _serializer = new Serializer();

            var datacardPath = new DatacardPath(_configFake);
            var datacardWriter = new DatacardWriter(datacardPath, _fileSystemFake, _serializer);
            var datacardExtractor = new DatacardExtractor(datacardPath, _fileSystemFake);
            var pluginFactoryFake = new PluginFactoryFake();
            _datacardStorage = new DatacardStorage(datacardPath, datacardWriter, datacardExtractor, pluginFactoryFake, _fileSystemFake, _serializer);
        }

        [Test]
        public async Task ShouldGetDatacardsFromFileSystem()
        {
            SetupDatacard("one", "one");
            SetupDatacard("two", "two");
            SetupDatacard("three", "three");

            var datacards = await _datacardStorage.GetDatacards();
            Assert.AreEqual(3, datacards.Length);
        }

        [Test]
        public async Task ShouldGetDatacardNameForEachDatacard()
        {
            SetupDatacard("One", "Next");

            var datacards = await _datacardStorage.GetDatacards();
            Assert.AreEqual("Next", datacards[0].Name);
        }

        [Test]
        public async Task ShouldGetDatacardIdForEachDatacard()
        {
            SetupDatacard("guid", "new");

            var datacards = await _datacardStorage.GetDatacards();
            Assert.AreEqual("guid", datacards[0].Id);
        }

        private void SetupDatacard(string directory, string name)
        {
            _fileSystemFake.Directories.Add(directory);

            var datacardModel = new DatacardModel(name: name);
            _fileSystemFake.FileText[Path.Combine(_configFake.DatacardsDirectory, directory, "Datacard.json")] = _serializer.Serialize(datacardModel);
        }
    }
}
