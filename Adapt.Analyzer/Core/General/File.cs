
using Adapt.Analyzer.Core.IoC;

namespace Adapt.Analyzer.Core.General
{
    public interface IFile
    {
        void WriteAllBytes(string path, byte[] bytes);
    }

    [Dependency(typeof(IFile))]
    public class File : IFile
    {
        public void WriteAllBytes(string path, byte[] bytes)
        {
            System.IO.File.WriteAllBytes(path, bytes);
        }
    }
}
