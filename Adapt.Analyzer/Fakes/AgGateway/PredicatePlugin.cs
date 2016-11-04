using System;
using System.Collections.Generic;
using AgGateway.ADAPT.ApplicationDataModel.ADM;

namespace Fakes.AgGateway
{
    public class PredicatePlugin : IPlugin
    {
        private readonly Func<string, Properties, bool> _predicate;
        public string Name { get; set; }
        public string Version { get; set; }
        public string Owner { get; set; }
        public List<ImportArgs> ImportArgs { get; }
        public List<ExportArgs> ExportArgs { get; } 
        public List<ApplicationDataModel> DataModels { get; }

        public PredicatePlugin(Func<string, Properties, bool> predicate = null)
        {
            _predicate = predicate ?? ((s, p) => true);
            ImportArgs = new List<ImportArgs>();
            ExportArgs = new List<ExportArgs>();
            DataModels = new List<ApplicationDataModel>();
        }

        public void Initialize(string args = null)
        {
            
        }

        public bool IsDataCardSupported(string dataPath, Properties properties = null)
        {
            return _predicate(dataPath, properties);
        }

        public IList<IError> ValidateDataOnCard(string dataPath, Properties properties = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<ApplicationDataModel> Import(string dataPath, Properties properties = null)
        {
            ImportArgs.Add(new ImportArgs(dataPath, properties));
            return DataModels;
        }

        public void Export(ApplicationDataModel dataModel, string exportPath, Properties properties = null)
        {
            ExportArgs.Add(new ExportArgs(dataModel, exportPath, properties));
        }

        public Properties GetProperties(string dataPath)
        {
            throw new System.NotImplementedException();
        }
    }
}
