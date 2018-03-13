using DataSuit.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using DataSuit.Providers;

namespace DataSuit.Tests
{
    public class SettingsTest
    {
        [Fact]
        public void AddProviderWithNull()
        {
            ISettings settings = new Settings();

            settings.AddProvider("Test", null);

            Assert.Equal(settings.Providers.Keys.ElementAt(0), "test");
        }

        [Fact]
        public void AddProviderWithDictionary()
        {
            ISettings settings = new Settings();

            var providers = new Dictionary<string, IDataProvider>();
            providers.Add("Test", null);

            settings.AddProvider(providers);

            Assert.Equal(settings.Providers.Keys.ElementAt(0), "test");
        }

    }
}