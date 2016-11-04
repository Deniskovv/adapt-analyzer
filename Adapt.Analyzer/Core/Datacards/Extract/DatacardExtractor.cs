using System.IO;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analyzer.Core.Datacards.Extract
{
    public interface IDatacardExtractor
    {
        string Extract(string datacardId);
    }

    public class DatacardExtractor : IDatacardExtractor
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDatacardPath _datacardPath;

        public DatacardExtractor()
            : this(new DatacardPath(), new FileSystem())
        {
            
        }

        public DatacardExtractor(IDatacardPath datacardPath, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _datacardPath = datacardPath;
        }

        public string Extract(string datacardId)
        {
            var destination = _datacardPath.GetExtractPath(datacardId);
            if (_fileSystem.DirectoryExists(destination))
                return destination;

            var zipFilePath = _datacardPath.GetZipFilePath(datacardId);
            _fileSystem.ExtractZip(zipFilePath, destination);
            return destination;
        }
    }
}