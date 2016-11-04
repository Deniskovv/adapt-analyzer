
using System.IO.Compression;

namespace Adapt.Analyzer.Core.General
{
    public interface IFile
    {
        void WriteAllBytes(string path, byte[] bytes);
        void ExtractZip(string zipFilePath, string destinationPath);
    }

    public class File : IFile
    {
        public void WriteAllBytes(string path, byte[] bytes)
        {
            System.IO.File.WriteAllBytes(path, bytes);
        }

        public void ExtractZip(string zipFilePath, string destinationPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, destinationPath);
        }
    }
}
