namespace Adapt.Analyzer.Core.Datacards
{
    public interface IDatacardFactory
    {
        IDatacard Create(string id);
    }

    public class DatacardFactory : IDatacardFactory
    {
        public IDatacard Create(string id)
        {
            return new Datacard(id);
        }
    }
}