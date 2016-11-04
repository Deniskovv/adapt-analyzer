using System.IO;
using Adapt.Analyzer.Core.General;
using File = Adapt.Analyzer.Core.General.File;

namespace Adapt.Analyzer.Core.Datacards.Extract
{
    public interface IDatacardExtractor
    {
        string Extract(string datacardId);
    }

    public class DatacardExtractor : IDatacardExtractor
    {
        private readonly IFile _file;
        private readonly IDatacardPath _datacardPath;

        public DatacardExtractor()
            : this(new DatacardPath(), new File())
        {
            
        }

        public DatacardExtractor(IDatacardPath datacardPath, IFile file)
        {
            _file = file;
            _datacardPath = datacardPath;
        }

        public string Extract(string datacardId)
        {

            var zipFilePath = _datacardPath.GetZipFilePath(datacardId);
            var destination = _datacardPath.GetExtractPath(datacardId);
            _file.ExtractZip(zipFilePath, destination);
            return destination;
        }
    }
}