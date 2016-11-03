using System;
using System.IO;
using NUnit.Framework;
using File = Adapt.Analyzer.Core.General.File;

namespace Adapt.Analyzer.Core.Test.General
{
    [TestFixture]
    public class FileTest
    {
        private string _filePath;
        private File _file;

        [SetUp]
        public void Setup()
        {
            _file = new File();
        }

        [Test]
        public void WriteAllBytesShouldWriteBytesToFile()
        {
            var bytes = new byte[] {34, 34, 1, 23, 5, 4, 2};
            _filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            _file.WriteAllBytes(_filePath, bytes);
            Assert.AreEqual(bytes, System.IO.File.ReadAllBytes(_filePath));
        }

        [TearDown]
        public void Teardown()
        {
            if (System.IO.File.Exists(_filePath))
                System.IO.File.Delete(_filePath);
        }
    }
}
