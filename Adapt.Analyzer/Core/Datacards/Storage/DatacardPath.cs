using System.IO;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analyzer.Core.Datacards.Storage
{
    public interface IDatacardPath
    {
        string GetZipFilePath(string id);
        string GetExtractPath(string id);
        string GetDatacardsPath();
        string GetDatacardPath(string id);
    }

    public class DatacardPath : IDatacardPath
    {
        private readonly string _datacardsDirectory;

        public DatacardPath()
            : this(new Config())
        {
            
        }

        public DatacardPath(IConfig config)
        {
            _datacardsDirectory = config.GetSetting("datacards-dir");
        }

        public string GetZipFilePath(string id)
        {
            return Path.Combine(_datacardsDirectory, id, "Datacard.zip");
        }

        public string GetExtractPath(string id)
        {
            return Path.Combine(_datacardsDirectory, id, "Extracted");
        }

        public string GetDatacardPath(string id)
        {
            return Path.Combine(_datacardsDirectory, id);
        }

        public string GetDatacardsPath()
        {
            return _datacardsDirectory;
        }
    }
}
