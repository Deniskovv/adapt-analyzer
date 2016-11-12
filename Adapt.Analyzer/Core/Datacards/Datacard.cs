using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Metadata;
using Adapt.Analyzer.Core.Datacards.Models;
using Adapt.Analyzer.Core.Datacards.Plugins;
using Adapt.Analyzer.Core.Datacards.Totals;

namespace Adapt.Analyzer.Core.Datacards
{
    public interface IDatacard
    {
        string Id { get; }
        Task<Plugin[]> GetPlugins();
        Task<Metadata.Metadata> GetMetadata();
    }

    public class Datacard : IDatacard
    {
        private readonly IDatacardPluginFinder _datacardPluginFinder;
        private readonly IDatacardMetadataReader _datacardMetadataReader;
        private readonly IDatacardTotalsCalculator _datacardTotalsCalculator;
        
        public string Id { get; }

        public Datacard(string id)
            : this(id, new DatacardPluginFinder(), new DatacardMetadataReader(), new DatacardTotalsCalculator())
        {
            
        }

        public Datacard(string id, IDatacardPluginFinder datacardPluginFinder, IDatacardMetadataReader datacardMetadataReader, IDatacardTotalsCalculator datacardTotalsCalculator)
        {
            Id = id;
            _datacardPluginFinder = datacardPluginFinder;
            _datacardMetadataReader = datacardMetadataReader;
            _datacardTotalsCalculator = datacardTotalsCalculator;
        }

        public Task<Plugin[]> GetPlugins()
        {
            return _datacardPluginFinder.GetPlugins(Id);
        }

        public Task<Metadata.Metadata> GetMetadata()
        {
            return _datacardMetadataReader.Read(Id);
        }

        public Task<DatacardTotals> CalculateTotals()
        {
            return _datacardTotalsCalculator.Calculate(Id);
        }
    }
}