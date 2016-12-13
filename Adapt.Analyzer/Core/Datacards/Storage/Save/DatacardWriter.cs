using System;
using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Adapt.Analyzer.Core.Datacards.Storage.Save
{
    public interface IDatacardWriter
    {
        Task<string> Write(DatacardModel datacardModel);
    }

    public class DatacardWriter : IDatacardWriter
    {
        private readonly IFileSystem _fileSystem;
        private readonly ISerializer _serializer;
        private readonly IDatacardPath _datacardPath;

        public DatacardWriter()
            : this(new DatacardPath(), new FileSystem(), new Serializer())
        {
            
        }

        public DatacardWriter(IDatacardPath datacardPath, IFileSystem fileSystem, ISerializer serializer)
        {
            _fileSystem = fileSystem;
            _serializer = serializer;
            _datacardPath = datacardPath;
        }

        public Task<string> Write(DatacardModel datacardModel)
        {
            var id = Guid.NewGuid().ToString();
            EnsureDatacardDirectoryExists(id);
            WriteDatacardZipFile(id, datacardModel.Bytes);
            WriteDatacardJsonFile(id, datacardModel);
            return Task.FromResult(id);
        }

        private void WriteDatacardJsonFile(string id, DatacardModel datacardModel)
        {
            var datacardPath = _datacardPath.GetJsonFilePath(id);
            _fileSystem.WriteAllText(datacardPath, _serializer.Serialize(datacardModel));
        }

        private void WriteDatacardZipFile(string id, byte[] bytes)
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
