using Adapt.Analzyer.Api;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test
{
    [TestFixture]
    public class HttpConfigFactoryTest
    {
        [Test]
        public void ShouldUseCamelCaseJsonConverter()
        {
            var config = HttpConfigFactory.Create();
            Assert.IsInstanceOf<CamelCasePropertyNamesContractResolver>(config.Formatters.JsonFormatter.SerializerSettings.ContractResolver);
        }
    }
}
