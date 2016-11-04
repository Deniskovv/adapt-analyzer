using System.Configuration;

namespace Adapt.Analyzer.Core.General
{
    public interface IConfig
    {
        string GetSetting(string key);
    }

    public class Config : IConfig
    {
        public string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
