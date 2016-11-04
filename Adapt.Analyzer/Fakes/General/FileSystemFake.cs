using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class FileSystemFake : IFileSystem
    {
        public string WrittenFile { get; private set; }
        public byte[] WrittenBytes { get; private set; }

        public string ZipFilePath { get; private set; }
        public string DestinationPath { get; private set; }

        public bool DoesDirectoryExist { get; set; }

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

        public bool DirectoryExists(string directoryPath)
        {
            return DoesDirectoryExist;
        }
    }
}
