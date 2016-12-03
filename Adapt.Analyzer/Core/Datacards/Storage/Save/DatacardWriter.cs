using System;
using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analyzer.Core.Datacards.Storage.Save
{
    public interface IDatacardWriter
    {
        Task<string> Write(byte[] bytes);
    }

    public class DatacardWriter : IDatacardWriter
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDatacardPath _datacardPath;

        public DatacardWriter()
            : this(new DatacardPath(), new FileSystem())
        {
            
        }

        public DatacardWriter(IDatacardPath datacardPath, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _datacardPath = datacardPath;
        }

        public Task<string> Write(byte[] bytes)
        {
            var id = Guid.NewGuid().ToString();
            EnsureDatacardDirectoryExists(id);
            WriteDatacard(id, bytes);
            return Task.FromResult(id);
        }

        private void WriteDatacard(string id, byte[] bytes)
        {
            var datacardPath = _datacardPath.GetZipFilePath(id);
            _fileSystem.WriteAllBytes(datacardPath, bytes);
        }

        private void EnsureDatacardDirectoryExists(string id)
        {
            EnsureDatacardsDirectoryExists();

            var directoryPath = _datacardPath.GetDatacardPath(id);
            if (!_fileSystem.DirectoryExists(directoryPath))
                _fileSystem.CreateDirectory(directoryPath);
        }

        private void EnsureDatacardsDirectoryExists()
        {
            var datacardsPath = _datacardPath.GetDatacardsPath();
            if (!_fileSystem.DirectoryExists(datacardsPath))
                _fileSystem.CreateDirectory(datacardsPath);
        }
    }
}
