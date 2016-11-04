using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class FileFake : IFile
    {
        public string WrittenFile { get; private set; }
        public byte[] WrittenBytes { get; private set; }

        public string ZipFilePath { get; private set; }
        public string DestinationPath { get; private set; }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            WrittenFile = path;
            WrittenBytes = bytes;
        }

        public void ExtractZip(string zipFilePath, string destinationPath)
        {
            ZipFilePath = zipFilePath;
            DestinationPath = destinationPath;
        }
    }
}
