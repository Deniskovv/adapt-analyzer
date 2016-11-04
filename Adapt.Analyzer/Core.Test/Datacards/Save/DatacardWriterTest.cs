using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Save;
using Fakes.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.Datacards.Save
{
    [TestFixture]
    public class DatacardWriterTest
    {
        private const string DataCardsDirectory = "something goes here";
        private DatacardWriter _datacardWriter;
        private FileFake _fileFake;
        private ConfigFake _configFake;

        [SetUp]
        public void Setup()
        {
            _fileFake = new FileFake();
            _configFake = new ConfigFake();
            _configFake.SetSetting("datacards-dir", DataCardsDirectory);
            _datacardWriter = new DatacardWriter(_configFake, _fileFake);
        }

        [Test]
        public async Task UploadShouldSaveFileToDatacardsDirectory()
        {
            var bytes = new byte[] { 34, 23, 7, 6, 8, 23 };
        
            var result = await _datacardWriter.Write(bytes);
            Assert.AreEqual(_fileFake.WrittenFile, Path.Combine(DataCardsDirectory, result + ".zip"));
            Assert.AreEqual(_fileFake.WrittenBytes, bytes);
        }
    }
}
