using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Plugins.Models;
using Adapt.Analyzer.Core.Datacards.Totals.Models;

namespace Fakes.Datacards
{
    public class DatacardFake : IDatacard
    {
        private Plugin[] _plugins;
        private Metadata _metadata;
        private DatacardTotals _datacardTotals;
        private FieldBoundary[] _fieldBoundaries;
        private DatacardModel[] _datacardModels;

        public byte[] WrittenBytes { get; private set; }
        public string NewId { get; private set; }

        public Task<string> Save(DatacardModel datacardModel)
        {
            WrittenBytes = datacardModel.Bytes;
            NewId = Guid.NewGuid().ToString();
            return Task.FromResult(NewId);
        }

        public Task<DatacardModel[]> GetDatacards()
        {
            return Task.FromResult(_datacardModels);
        }

        public Task<Plugin[]> GetPlugins(string id)
        {
            return Task.FromResult(_plugins);
        }

        public Task<Metadata> GetMetadata(string id)
        {
            return Task.FromResult(_metadata);
        }

        public Task<DatacardTotals> CalculateTotals(string id)
        {
            return Task.FromResult(_datacardTotals);
        }

        public Task<FieldBoundary[]> GetFieldBoundaries(string id)
        {
            return Task.FromResult(_fieldBoundaries);
        }

        public void SetupPlugins(Plugin[] plugins)
        {
            _plugins = plugins;
        }

        public void SetupMetadata(Metadata metadata)
        {
            _metadata = metadata;
        }

        public void SetupTotals(DatacardTotals totals)
        {
            _datacardTotals = totals;
        }

        public void SetupFieldBoundaries(FieldBoundary[] fieldBoundaries)
        {
            _fieldBoundaries = fieldBoundaries;
        }

        public void SetupDatacardModels(DatacardModel[] datacardModels)
        {
            _datacardModels = datacardModels;
        }
    }
}
