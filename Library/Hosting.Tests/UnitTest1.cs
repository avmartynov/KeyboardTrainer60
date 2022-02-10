using Microsoft.Extensions.Configuration;
using Xunit;

namespace Library.Hosting.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Читаем_appSettings_как_конфигурацию()
        {
            var config = new ConfigurationBuilder().AddXmlFile("Library.Hosting.Tests.dll.config").Build();
            var val = config["appSettings:add:key"];

            Assert.Equal("Abc", val);

            val = config["appSettings:add:value"];
            Assert.Equal("123", val);
        }
    }
}