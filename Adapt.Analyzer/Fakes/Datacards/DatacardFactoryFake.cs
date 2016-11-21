using Adapt.Analyzer.Core.Datacards;

namespace Fakes.Datacards
{
    public class DatacardFactoryFake : IDatacardFactory
    {
        private readonly DatacardFake _datacardFake;

        public DatacardFactoryFake(DatacardFake datacardFake)
        {
            _datacardFake = datacardFake;
        }

        public IDatacard Create()
        {
            return _datacardFake;
        }
    }
}
