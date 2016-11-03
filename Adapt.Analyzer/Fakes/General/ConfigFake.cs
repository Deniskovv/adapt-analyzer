using System.Collections.Generic;
using Adapt.Analyzer.Core.General;

namespace Fakes.General
{
    public class ConfigFake : IConfig
    {
        private readonly Dictionary<string, string> _settings;

        public ConfigFake()
        {
            _settings = new Dictionary<string, string>();
        }

        public string GetSetting(string key)
        {
            return _settings[key];
        }

        public void SetSetting(string key, string value)
        {
            _settings[key] = value;
        }
    }
}
