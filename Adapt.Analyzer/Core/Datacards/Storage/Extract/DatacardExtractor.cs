using System.Threading;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analyzer.Core.Datacards.Storage.Extract
{
    public interface IDatacardExtractor
    {
        Task<string> Extract(string datacardId);
    }

    public class DatacardExtractor : IDatacardExtractor
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
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

        public async Task<string> Extract(string datacardId)
        {
            await Semaphore.WaitAsync();
            try
            {
                return ExtractDatacard(datacardId);
            }
            finally
            {
                Semaphore.Release();
            }
            
        }

        private string ExtractDatacard(string datacardId)
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