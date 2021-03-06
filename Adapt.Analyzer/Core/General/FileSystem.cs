﻿using System.IO;
using System.IO.Compression;

namespace Adapt.Analyzer.Core.General
{
    public interface IFileSystem
    {
        void WriteAllBytes(string path, byte[] bytes);
        void ExtractZip(string zipFilePath, string destinationPath);
        bool DirectoryExists(string directoryPath);
        void CreateDirectory(string directoryPath);
        void WriteAllText(string path, string content);
        string[] GetDirectories(string path);
        string ReadAllText(string path);
    }

    public class FileSystem : IFileSystem
    {
        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public void ExtractZip(string zipFilePath, string destinationPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, destinationPath);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }

        public void WriteAllText(string path, string content)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetDirectories(string path)
        {
            throw new System.NotImplementedException();
        }

        public string ReadAllText(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
