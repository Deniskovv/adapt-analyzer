using System;
using System.IO;
using System.IO.Compression;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.General
{
    [TestFixture]
    public class FileTest
    {
        private string _filePath;
        private string _directoryPath;
        private Core.General.File _file;

        [SetUp]
        public void Setup()
        {
            _file = new Core.General.File();
        }

        [Test]
        public void WriteAllBytesShouldWriteBytesToFile()
        {
            var bytes = new byte[] {34, 34, 1, 23, 5, 4, 2};
            _filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            _file.WriteAllBytes(_filePath, bytes);
            Assert.AreEqual(bytes, File.ReadAllBytes(_filePath));
        }

        [Test]
        public void ExtractZipShouldExtractZipFile()
        {
            var zipFilePath = CreateZipFile();
            var destination = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            _file.ExtractZip(zipFilePath, destination);
            Assert.AreEqual(Directory.GetFiles(_directoryPath).Length, Directory.GetFiles(destination).Length);
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            if (Directory.Exists(_directoryPath))
                Directory.Delete(_directoryPath, true);
        }

        private string CreateZipFile()
        {
            _directoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_directoryPath);
            File.WriteAllText(Path.Combine(_directoryPath, "file1.txt"), "data");
            File.WriteAllText(Path.Combine(_directoryPath, "file2.txt"), "other");
            File.WriteAllText(Path.Combine(_directoryPath, "file3.txt"), "not here");

            var zipFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
            ZipFile.CreateFromDirectory(_directoryPath, zipFilePath);
            return zipFilePath;
        }
    }
}
