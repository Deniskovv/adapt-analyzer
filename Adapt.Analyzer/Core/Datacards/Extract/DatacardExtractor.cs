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
        private readonly string _datacardsDirectory;

        public DatacardExtractor()
            : this(new Config(), new File())
        {
            
        }

        public DatacardExtractor(IConfig config, IFile file)
        {
            _file = file;
            _datacardsDirectory = config.GetSetting("datacards-dir");
        }

        public string Extract(string datacardId)
        {
            
            var zipFilePath = Path.Combine(_datacardsDirectory, datacardId + ".zip");
            var destination = Path.Combine(_datacardsDirectory, datacardId);
            _file.ExtractZip(zipFilePath, destination);
            return destination;
        }
    }
}