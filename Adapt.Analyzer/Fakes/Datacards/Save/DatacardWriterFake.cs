using System;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.Datacards.Save;

namespace Fakes.Datacards.Save
{
    public class DatacardWriterFake : IDatacardWriter
    {
        public string Id { get; private set; }
        public byte[] WrittenBytes { get; private set; }

        public Task<string> Write(byte[] bytes)
        {
            WrittenBytes = bytes;
            Id = Guid.NewGuid().ToString();
            return Task.FromResult(Id);
        }
    }
}
