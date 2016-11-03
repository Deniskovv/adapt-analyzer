using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class FileFake : IFile
    {
        public string WrittenFile { get; private set; }
        public byte[] WrittenBytes { get; private set; }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            WrittenFile = path;
            WrittenBytes = bytes;
        }
    }
}
