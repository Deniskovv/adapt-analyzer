using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Boundaries;
using Adapt.Analyzer.Core.Datacards.Boundaries.Models;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Plugins;
using Adapt.Analyzer.Core.Datacards.Plugins.Models;
using Adapt.Analyzer.Core.Datacards.Storage;
using Adapt.Analyzer.Core.Datacards.Storage.Models;
using Adapt.Analyzer.Core.Datacards.Totals.Calculators;
using Adapt.Analyzer.Core.Datacards.Totals.Models;

namespace Adapt.Analyzer.Core.Datacards
{
    public interface IDatacard
    {
        Task<Plugin[]> GetPlugins(string id);
        Task<Metadata.Metadata> GetMetadata(string id);
        Task<DatacardTotals> CalculateTotals(string id);
        Task<FieldBoundary[]> GetFieldBoundaries(string id);
    }

    public class Datacard : IDatacard
    {
        private readonly IDatacardStorage _storage;
        private readonly IDatacardPluginFinder _datacardPluginFinder;
        private readonly IDatacardMetadataReader _datacardMetadataReader;
        private readonly IDatacardTotalsCalculator _datacardTotalsCalculator;
        private readonly IFieldBoundaryReader _fieldBoundaryReader;

        public Datacard()
            : this(new DatacardStorage(), new DatacardPluginFinder(), new DatacardMetadataReader(), new DatacardTotalsCalculator(), new FieldBoundaryReader())
        {
            
        }

        public Datacard(IDatacardStorage storage, IDatacardPluginFinder datacardPluginFinder, IDatacardMetadataReader datacardMetadataReader, IDatacardTotalsCalculator datacardTotalsCalculator, IFieldBoundaryReader fieldBoundaryReader)
        {
            _storage = storage;
            _datacardPluginFinder = datacardPluginFinder;
            _datacardMetadataReader = datacardMetadataReader;
            _datacardTotalsCalculator = datacardTotalsCalculator;
            _fieldBoundaryReader = fieldBoundaryReader;
        }

        public Task<Plugin[]> GetPlugins(string id)
        {
            return GetData(id, _datacardPluginFinder.GetPlugins);
        }

        public Task<Metadata.Metadata> GetMetadata(string id)
        {
            return GetData(id, _datacardMetadataReader.Read);
        }

        public Task<DatacardTotals> CalculateTotals(string id)
        {
            return GetData(id, _datacardTotalsCalculator.Calculate);
        }

        public Task<FieldBoundary[]> GetFieldBoundaries(string id)
        {
            return GetData(id, _fieldBoundaryReader.GetFieldBoundaries);
        }

        private async Task<T> GetData<T>(string id, Func<IEnumerable<StorageDataModel>, Task<T>> func)
        {
            var dataModels = await _storage.GetDataModels(id);
            return await func(dataModels);
        }
    }
}