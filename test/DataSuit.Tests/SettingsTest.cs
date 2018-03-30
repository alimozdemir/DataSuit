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
        public void UpdateProviderWithNull()
        {
            ISettings settings = new Settings();

            settings.AddProvider("Test", null);
            settings.AddProvider("Test", null);

            Assert.Equal(settings.Providers.Keys.ElementAt(0), "test");
            Assert.Equal(settings.Providers.Count, 1);
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
        [Fact]
        public void UpdateProviderWithDictionary()
        {
            ISettings settings = new Settings();

            var providers = new Dictionary<string, IDataProvider>();
            providers.Add("Test", null);

            settings.AddProvider(providers);

            var updateProviders = new Dictionary<string, IDataProvider>();
            updateProviders.Add("Test", null);
            settings.AddProvider(updateProviders);

            Assert.Equal(settings.Providers.Keys.ElementAt(0), "test");
            Assert.Equal(settings.Providers.Count, 1);
        }

        [Fact]
        public void RemoveProvider()
        {
            ISettings settings = new Settings();
            settings.AddProvider("Test", null);

            settings.RemoveProvider("Test");

            Assert.Equal(settings.Providers.Count, 0);
        }

        [Fact]
        public void ExportData()
        {
            ISettings settings = new Settings();

        }
    }
}