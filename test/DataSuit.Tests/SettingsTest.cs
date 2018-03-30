using DataSuit.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using DataSuit.Providers;
using Moq;
using DataSuit.Enums;

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
        public void ImportByJsonFieldWithStaticProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(string).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Static.ToString());
            mock.SetupGet(i => i.Value).Returns("Hello World");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as StaticProvider<string>;

            Assert.Equal(typeof(string), provider.TType);
            Assert.Equal(ProviderType.Static, provider.Type);
            Assert.Equal("Hello World", provider.Current);
        }

        [Fact]
        public void ImportByJsonFieldWithRandomProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(int).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Random.ToString());
            mock.SetupGet(i => i.Value).Returns("[1, 2, 3]");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as CollectionProvider<int>;

            Assert.Equal(typeof(int), provider.TType);
            Assert.Equal(ProviderType.Random, provider.Type);
            Assert.Contains(1, provider.Collection);
            Assert.Contains(2, provider.Collection);
            Assert.Contains(3, provider.Collection);
        }


        [Fact]
        public void ImportByJsonFieldWithSequentialProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(int).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Sequential.ToString());
            mock.SetupGet(i => i.Value).Returns("[1, 2, 3]");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as CollectionProvider<int>;

            Assert.Equal(typeof(int), provider.TType);
            Assert.Equal(ProviderType.Sequential, provider.Type);
            Assert.Contains(1, provider.Collection);
            Assert.Contains(2, provider.Collection);
            Assert.Contains(3, provider.Collection);
        }

        [Fact]
        public void ImportByJsonFieldWithRangeIntProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(int).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Range.ToString());
            mock.SetupGet(i => i.MinValue).Returns("3");
            mock.SetupGet(i => i.MaxValue).Returns("6");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as RangeIntProvider;

            Assert.Equal(typeof(int), provider.TType);
            Assert.Equal(ProviderType.Range, provider.Type);
            Assert.Equal(3, provider.MinValue);
            Assert.Equal(6, provider.MaxValue);
        }

        [Fact]
        public void ImportByJsonFieldWithRangeDoubleProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(double).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Range.ToString());
            mock.SetupGet(i => i.MinValue).Returns("3");
            mock.SetupGet(i => i.MaxValue).Returns("6");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as RangeDoubleProvider;

            Assert.Equal(typeof(double), provider.TType);
            Assert.Equal(ProviderType.Range, provider.Type);
            Assert.Equal(3d, provider.MinValue);
            Assert.Equal(6d, provider.MaxValue);
        }

        [Fact]
        public void ImportByJsonFieldWithPhoneProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(string).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Phone.ToString());
            mock.SetupGet(i => i.Value).Returns("(xxx) xx");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as PhoneProvider;

            Assert.Equal(typeof(string), provider.TType);
            Assert.Equal(ProviderType.Phone, provider.Type);
            Assert.Equal("(xxx) xx", provider.Format);
        }

        [Fact]
        public void ImportByJsonFieldWithDummyProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(string).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.DummyText.ToString());
            mock.SetupGet(i => i.Value).Returns("30");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as DummyTextProvider;

            Assert.Equal(typeof(string), provider.TType);
            Assert.Equal(ProviderType.DummyText, provider.Type);
            Assert.Equal(30, provider.MaxLength);
        }

        [Fact]
        public void ImportByJsonFieldWithIncrementalProvider()
        {
            ISettings settings = new Settings();
            var mock = new Mock<IJsonFieldSettings>();
            mock.SetupGet(i => i.T).Returns(typeof(int).ToString());
            mock.SetupGet(i => i.Type).Returns(ProviderType.Incremental.ToString());
            mock.SetupGet(i => i.Value).Returns("id");
            mock.SetupGet(i => i.Fields).Returns("key");

            settings.ImportByJsonField(mock.Object);

            var provider = settings.Providers.Values.FirstOrDefault() as IncrementalProvider;

            Assert.Equal(typeof(int), provider.TType);
            Assert.Equal(ProviderType.Incremental, provider.Type);
            Assert.Equal("id", provider.Prop);
        }
    }
}