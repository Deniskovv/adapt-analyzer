using System.Configuration;
using Adapt.Analyzer.Core.General;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.General
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void GetSettingShouldGetSettingFromConfiguration()
        {
            AddSetting("my-value", "this is a value");

            var config = new Config();
            var setting = config.GetSetting("my-value");
            Assert.AreEqual("this is a value", setting);
        }

        [TearDown]
        public void Teardown()
        {
            CleanupSettings();
        }

        private static void AddSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static void CleanupSettings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
