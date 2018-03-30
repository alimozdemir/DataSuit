using System;
using System.Collections.Generic;
using DataSuit.Enums;
using DataSuit.Infrastructures;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests
{
    public class JsonFieldSettingsTests
    {
        [Fact]
        public void ConstructorWithStaticProvider()
        {
            IDataProvider provider = new StaticProvider<string>("Hello World");
            var settings = new JsonFieldSettings("name", provider);

            Assert.Equal("name", settings.Fields);
            Assert.Equal(typeof(string).ToString(), settings.T);
            Assert.Equal("Hello World", settings.Value);
            Assert.Equal(ProviderType.Static.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithRandomProvider()
        {
            var list = new List<int>() { 3, 5, 7 };
            IDataProvider provider = new CollectionProvider<int>(list, ProviderType.Random);
            var settings = new JsonFieldSettings("age", provider);

            Assert.Equal("age", settings.Fields);
            Assert.Equal(typeof(int).ToString(), settings.T);
            Assert.All(list, i =>
            {
                Assert.Contains(i, (IEnumerable<int>)settings.Value);
            });
            Assert.Equal(ProviderType.Random.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithSequentialProvider()
        {
            var list = new List<int>() { 3, 5, 7 };
            IDataProvider provider = new CollectionProvider<int>(list);
            var settings = new JsonFieldSettings("age", provider);

            Assert.Equal("age", settings.Fields);
            Assert.Equal(typeof(int).ToString(), settings.T);
            Assert.Equal(list, settings.Value);
            Assert.Equal(ProviderType.Sequential.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithRangeIntProvider()
        {
            IDataProvider provider = new RangeIntProvider(3, 6);
            var settings = new JsonFieldSettings("age", provider);

            Assert.Equal("age", settings.Fields);
            Assert.Equal(typeof(int).ToString(), settings.T);
            Assert.Equal(3, settings.MinValue);
            Assert.Equal(6, settings.MaxValue);
            Assert.Equal(ProviderType.Range.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithRangeDoubleProvider()
        {
            IDataProvider provider = new RangeDoubleProvider(3, 6);
            var settings = new JsonFieldSettings("age", provider);

            Assert.Equal("age", settings.Fields);
            Assert.Equal(typeof(double).ToString(), settings.T);
            Assert.Equal(3d, settings.MinValue);
            Assert.Equal(6d, settings.MaxValue);
            Assert.Equal(ProviderType.Range.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithPhoneProvider()
        {
            IDataProvider provider = new PhoneProvider("(0-xxx) xxx xx xx");
            var settings = new JsonFieldSettings("phone", provider);

            Assert.Equal("phone", settings.Fields);
            Assert.Equal("(0-xxx) xxx xx xx", settings.Value);
            Assert.Equal(ProviderType.Phone.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithDummyTextProvider()
        {
            IDataProvider provider = new DummyTextProvider(10);
            var settings = new JsonFieldSettings("text", provider);

            Assert.Equal("text", settings.Fields);
            Assert.Equal(10, settings.Value);
            Assert.Equal(ProviderType.DummyText.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithIncrementalProvider()
        {
            IDataProvider provider = new IncrementalProvider("id");
            var settings = new JsonFieldSettings("id", provider);

            Assert.Equal("id", settings.Fields);
            Assert.Equal("id", settings.Value);
            Assert.Equal(ProviderType.Incremental.ToString(), settings.Type);
        }

        [Fact]
        public void ConstructorWithFuncProvider()
        {
            Func<string> testFunction = () =>
            {
                return string.Empty;
            };

            IDataProvider provider = new FuncProvider<string>(testFunction);
            var settings = new JsonFieldSettings("id", provider);

            Assert.Equal("id", settings.Fields);
            Assert.Equal(ProviderType.Func.ToString(), settings.Type);
        }
    }
}