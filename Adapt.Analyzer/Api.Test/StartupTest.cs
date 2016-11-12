using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test
{
    [TestFixture]
    public class StartupTest
    {
        private TestServer _testServer;
        private string _indexFilePath;

        [SetUp]
        public void Setup()
        {
            _indexFilePath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
            _testServer = TestServer.Create<Startup>();
        }

        [Test]
        public async Task ShouldConfigureHello()
        {
            var response = await _testServer.HttpClient.GetAsync("/hello");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task ShouldConfigureFileServer()
        {
            File.WriteAllText(_indexFilePath, "It works!!!");
            var response = await _testServer.HttpClient.GetAsync("/");
            Assert.AreEqual("It works!!!", await response.Content.ReadAsStringAsync());
        }

        [TearDown]
        public void Teardown()
        {
            _testServer.Dispose();
            if (File.Exists(_indexFilePath))
                File.Delete(_indexFilePath);
        }
    }
}
