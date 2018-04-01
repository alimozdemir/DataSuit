using DataSuit.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using DataSuit.Providers;
using Moq;
using DataSuit.Enums;
using Newtonsoft.Json;
using DataSuit.Infrastructures;
using System;

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

        [Fact]
        public void ExportWithNoProviders()
        {
            ISettings settings = new Settings();
            var data = settings.Export();

            var jsonSettings = JsonConvert.DeserializeObject<JsonSettings>(data);

            Assert.Equal(settings.Relationship.Type.ToString(), jsonSettings.Relationship.Type);
            Assert.Equal(settings.Relationship.Value, jsonSettings.Relationship.Value);
            Assert.Equal(0, jsonSettings.Providers.Count());
        }

        [Fact]
        public void ExportWithAProvider()
        {
            ISettings settings = new Settings();
            settings.Relationship = (RelationshipMap.MaxRandom, 5);
            var provider = new StaticProvider<string>("Hello World");
            settings.AddProvider("Name", provider);

            var data = settings.Export();
            var jsonSettings = JsonConvert.DeserializeObject<JsonSettings>(data);

            Assert.Equal(settings.Relationship.Type.ToString(), jsonSettings.Relationship.Type);
            Assert.Equal(settings.Relationship.Value, jsonSettings.Relationship.Value);
            Assert.Equal(1, jsonSettings.Providers.Count());

            Assert.All(jsonSettings.Providers, i =>
            {
                Assert.Equal(ProviderType.Static.ToString(), i.Type);
                Assert.Equal("Hello World", i.Value);
                Assert.Equal("name", i.Fields);
            });
        }


        [Fact]
        public void ExportWithProvidersInMultipleFields()
        {
            ISettings settings = new Settings();
            settings.Relationship = (RelationshipMap.MaxRandom, 5);
            var provider = new StaticProvider<string>("Hello World");
            settings.AddProvider("Name", provider);
            settings.AddProvider("Message", provider);

            var data = settings.Export();
            var jsonSettings = JsonConvert.DeserializeObject<JsonSettings>(data);

            Assert.Equal(settings.Relationship.Type.ToString(), jsonSettings.Relationship.Type);
            Assert.Equal(settings.Relationship.Value, jsonSettings.Relationship.Value);
            Assert.Equal(1, jsonSettings.Providers.Count());

            Assert.All(jsonSettings.Providers, i =>
            {
                Assert.Equal(ProviderType.Static.ToString(), i.Type);
                Assert.Equal("Hello World", i.Value);
                Assert.Contains("name", i.Fields);
                Assert.Contains("message", i.Fields);
            });
        }

        [Fact]
        public void ExportWithDifferentProviders()
        {
            ISettings settings = new Settings();
            settings.Relationship = (RelationshipMap.MaxRandom, 5);
            var staticProvider = new StaticProvider<string>("Hello World");
            var rangeProvider = new RangeIntProvider(10, 50);
            settings.AddProvider("Name", staticProvider);
            settings.AddProvider("Age", rangeProvider);

            var data = settings.Export();
            var jsonSettings = JsonConvert.DeserializeObject<JsonSettings>(data);
            var jStaticProvider = jsonSettings.Providers.FirstOrDefault(i => i.Type == ProviderType.Static.ToString());
            var jRangeProvider = jsonSettings.Providers.FirstOrDefault(i => i.Type == ProviderType.Range.ToString());

            Assert.Equal(settings.Relationship.Type.ToString(), jsonSettings.Relationship.Type);
            Assert.Equal(settings.Relationship.Value, jsonSettings.Relationship.Value);
            Assert.Equal(2, jsonSettings.Providers.Count());

            Assert.Equal(staticProvider.Current, jStaticProvider.Value);
            Assert.Equal(rangeProvider.MinValue, Convert.ToInt32(jRangeProvider.MinValue));
            Assert.Equal(rangeProvider.MaxValue, Convert.ToInt32(jRangeProvider.MaxValue));
        }
        // I'm not sure about following tests
        [Fact]
        public void ImportWithProviders()
        {
            var data = "{\n  \"Relationship\": {\n    \"Type\": \"Constant\",\n    \"Value\": 3\n  },\n  \"Providers\": []\n}";
            //var data = "{\n  \"Relationship\": {\n    \"Type\": \"MaxRandom\",\n    \"Value\": 5\n  },\n  \"Providers\": [\n    {\n      \"Fields\": \"name\",\n      \"Type\": \"Static\",\n      \"Value\": \"Hello World\",\n      \"T\": \"System.String\"\n    }\n  ]\n}";

            ISettings settings = new Settings();
            settings.Import(data);

            Assert.Equal(RelationshipMap.Constant, settings.Relationship.Type);
            Assert.Equal(3, settings.Relationship.Value);
            Assert.Equal(0, settings.Providers.Count);
        }

        [Fact]
        public void TestName()
        {
            //var data = "{\n  \"Relationship\": {\n    \"Type\": \"Constant\",\n    \"Value\": 3\n  },\n  \"Providers\": []\n}";
            var data = "{\n  \"Relationship\": {\n    \"Type\": \"MaxRandom\",\n    \"Value\": 5\n  },\n  \"Providers\": [\n    {\n      \"Fields\": \"name\",\n      \"Type\": \"Static\",\n      \"Value\": \"Hello World\",\n      \"T\": \"System.String\"\n    }\n  ]\n}";

            ISettings settings = new Settings();
            settings.Import(data);
            var provider = settings.Providers.Values.FirstOrDefault();
            var fields = settings.Providers.Keys.FirstOrDefault();

            Assert.Equal(RelationshipMap.MaxRandom, settings.Relationship.Type);
            Assert.Equal(5, settings.Relationship.Value);
            Assert.Equal(1, settings.Providers.Count);
            Assert.Equal("name", fields);
            Assert.Equal(ProviderType.Static, provider.Type);
            Assert.Equal("Hello World", provider.Current);

        }
    }
}