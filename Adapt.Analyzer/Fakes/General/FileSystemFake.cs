using System.Collections.Generic;
using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class FileSystemFake : IFileSystem
    {
        public List<string> WrittenFiles { get; }
        public byte[] WrittenBytes { get; private set; }
        public string WrittenText { get; private set; }

        public string ZipFilePath { get; private set; }
        public string DestinationPath { get; private set; }

        public bool DoesDirectoryExist { get; set; }
        public List<string> CreatedDirectories { get; }
        public List<string> Directories { get; }
        public string ParentDirectory { get; private set; }
        public Dictionary<string, string> FileText { get; }

        public FileSystemFake()
        {
            FileText = new Dictionary<string, string>();
            Directories = new List<string>();
            WrittenFiles = new List<string>();
            CreatedDirectories = new List<string>();
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            WrittenFiles.Add(path);
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

        public void CreateDirectory(string directoryPath)
        {
            CreatedDirectories.Add(directoryPath);
        }

        public void WriteAllText(string path, string content)
        {
            WrittenFiles.Add(path);
            WrittenText = content;
        }

        public string[] GetDirectories(string directoryPath)
        {
            ParentDirectory = directoryPath;
            return Directories.ToArray();
        }

        public string ReadAllText(string path)
        {
            return FileText[path];
        }
    }
}
