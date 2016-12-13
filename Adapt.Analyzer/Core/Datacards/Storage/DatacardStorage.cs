using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Extract;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.Datacards.Storage.Save;
using Adapt.Analyzer.Core.General;
using AgGateway.ADAPT.PluginManager;
using FileSystem = Adapt.Analyzer.Core.General.FileSystem;
using IFileSystem = Adapt.Analyzer.Core.General.IFileSystem;
using PluginFactory = Adapt.Analyzer.Core.General.PluginFactory;

namespace Adapt.Analyzer.Core.Datacards.Storage
{
    public interface IDatacardStorage
    {
        Task<string> Save(DatacardModel datacardModel);
        Task<StorageDataModel[]> GetDataModels(string id);
        Task<DatacardModel[]> GetDatacards();
    }

    public class DatacardStorage : IDatacardStorage
    {
        private readonly IDatacardWriter _writer;
        private readonly IDatacardExtractor _extractor;
        private readonly IPluginFactory _pluginFactory;
        private readonly IFileSystem _fileSystem;
        private readonly ISerializer _serializer;
        private readonly IDatacardPath _datacardPath;

        public DatacardStorage()
            : this(new DatacardPath(), new DatacardWriter(), new DatacardExtractor(), PluginFactory.Create(), new FileSystem(), new Serializer())
        {
            
        }

        public DatacardStorage(IDatacardPath datacardPath, IDatacardWriter writer, IDatacardExtractor extractor, IPluginFactory pluginFactory, IFileSystem fileSystem, ISerializer serializer)
        {
            _datacardPath = datacardPath;
            _writer = writer;
            _extractor = extractor;
            _pluginFactory = pluginFactory;
            _fileSystem = fileSystem;
            _serializer = serializer;
        }

        public Task<string> Save(DatacardModel datacardModel)
        {
            return _writer.Write(datacardModel);
        }

        public async Task<StorageDataModel[]> GetDataModels(string id)
        {
            var datacardPath = await _extractor.Extract(id);
            var plugins = _pluginFactory.GetSupportedPlugins(datacardPath);
            var storageModels = plugins.Select(p => new StorageDataModel(datacardPath, p));
            return storageModels.ToArray();
        }

        public Task<DatacardModel[]> GetDatacards()
        {
            var datacardsPath = _datacardPath.GetDatacardsPath();
            var datacards = _fileSystem.GetDirectories(datacardsPath)
                .Select(CreateDatacardModel)
                .ToArray();
            return Task.FromResult(datacards);
        }

        private DatacardModel CreateDatacardModel(string directory)
        {
            var id = new DirectoryInfo(directory).Name;
            var jsonPath = _datacardPath.GetJsonFilePath(id);
            var json = _fileSystem.ReadAllText(jsonPath);
            var datacardModel = _serializer.Deserialize<DatacardModel>(json);
            return new DatacardModel(id, datacardModel.Name);
        }
    }
}
