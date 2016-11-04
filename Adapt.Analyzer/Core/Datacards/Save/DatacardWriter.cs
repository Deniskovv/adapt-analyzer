﻿using System;
using System.IO;
using System.Threading.Tasks;
using Adapt.Analyzer.Core.General;

namespace Adapt.Analyzer.Core.Datacards.Save
{
    public interface IDatacardWriter
    {
        Task<string> Write(byte[] bytes);
    }

    public class DatacardWriter : IDatacardWriter
    {
        private readonly IFile _file;
        private readonly string _datacardsDirectory;

        public DatacardWriter()
            : this(new Config(), new General.File())
        {
            
        }

        public DatacardWriter(IConfig config, IFile file)
        {
            _file = file;
            _datacardsDirectory = config.GetSetting("datacards-dir");
        }

        public Task<string> Write(byte[] bytes)
        {
            var id = Guid.NewGuid();
            var datacardPath = Path.Combine(_datacardsDirectory, id + ".zip");
            _file.WriteAllBytes(datacardPath, bytes);
            return Task.FromResult(id.ToString());
        }
    }
}