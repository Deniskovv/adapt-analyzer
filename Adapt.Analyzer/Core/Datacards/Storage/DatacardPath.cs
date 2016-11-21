using System.IO;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analyzer.Core.Datacards.Storage
{
    public interface IDatacardPath
    {
        string GetZipFilePath(string id);
        string GetExtractPath(string id);
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
            return Path.Combine(_datacardsDirectory, id + ".zip");
        }

        public string GetExtractPath(string id)
        {
            return Path.Combine(_datacardsDirectory, id);
        }
    }
}
