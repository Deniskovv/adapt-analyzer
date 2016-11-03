using System.Configuration;
using Adapt.Analyzer.Core.IoC;

namespace Adapt.Analyzer.Core.General
{
    public interface IConfig
    {
        string GetSetting(string key);
    }

    [Dependency(typeof(IConfig))]
    public class Config : IConfig
    {
        public string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
