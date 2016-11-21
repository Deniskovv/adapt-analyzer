namespace Adapt.Analyzer.Core.Datacards
{
    public interface IDatacardFactory
    {
        IDatacard Create();
    }

    public class DatacardFactory : IDatacardFactory
    {
        public IDatacard Create()
        {
            return new Datacard();
        }
    }
}