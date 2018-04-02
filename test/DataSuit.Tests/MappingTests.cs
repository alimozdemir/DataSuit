using System;
using System.Collections.Generic;
using System.Linq;
using DataSuit.Enums;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests
{
    public class MappingConfig
    {
        public static IEnumerable<object[]> TestCases
        {
            get
            {
                var items = new List<object[]>();

                items.Add(new object[] { new Mapping() });
                items.Add(new object[] { new Mapping<MappingConfig>() });

                return items;
            }
        }
    }
    public class MappingTests
    {
        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingSetIntStaticProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Set("Age", 30);
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Age", provider.Key);
            Assert.Equal(ProviderType.Static, provider.Value.Type);
            Assert.Equal(typeof(int), provider.Value.TType);
            Assert.Equal(30, provider.Value.Current);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingSetStringStaticProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Set("Name", "John");
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Name", provider.Key);
            Assert.Equal(ProviderType.Static, provider.Value.Type);
            Assert.Equal(typeof(string), provider.Value.TType);
            Assert.Equal("John", provider.Value.Current);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingRangeIntProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Range("Age", 20, 40);
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Age", provider.Key);
            Assert.Equal(ProviderType.Range, provider.Value.Type);
            Assert.Equal(typeof(int), provider.Value.TType);
            Assert.Equal(20, ((RangeIntProvider)provider.Value).MinValue);
            Assert.Equal(40, ((RangeIntProvider)provider.Value).MaxValue);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingRangeDoubleProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Range("Age", 20d, 40d);
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Age", provider.Key);
            Assert.Equal(ProviderType.Range, provider.Value.Type);
            Assert.Equal(typeof(double), provider.Value.TType);
            Assert.Equal(20d, ((RangeDoubleProvider)provider.Value).MinValue);
            Assert.Equal(40d, ((RangeDoubleProvider)provider.Value).MaxValue);
        }


        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingPhoneProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Phone("Phone", "(0-xxx) xx");
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Phone", provider.Key);
            Assert.Equal(ProviderType.Phone, provider.Value.Type);
            Assert.Equal(typeof(string), provider.Value.TType);
            Assert.Equal("(0-xxx) xx", ((PhoneProvider)provider.Value).Format);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingIncrementalProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Incremental("Id");
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Id", provider.Key);
            Assert.Equal(ProviderType.Incremental, provider.Value.Type);
            Assert.Equal(typeof(int), provider.Value.TType);
            Assert.Equal("Id", ((IncrementalProvider)provider.Value).Prop);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingGuidProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Guid("Id");
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Id", provider.Key);
            Assert.Equal(ProviderType.Func, provider.Value.Type);
            Assert.Equal(typeof(Guid), provider.Value.TType);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingDummyProvider(IMapping map)
        {
            var providers = map.GetFieldsWithProviders;
            var result = map.Dummy("Text", 30);
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Text", provider.Key);
            Assert.Equal(ProviderType.DummyText, provider.Value.Type);
            Assert.Equal(typeof(string), provider.Value.TType);
            Assert.Equal(30, ((DummyTextProvider)provider.Value).MaxLength);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingCollectionSequentialProvider(IMapping map)
        {
            List<int> data = new List<int>() { 1, 2, 3, 4 };
            var providers = map.GetFieldsWithProviders;
            var result = map.Collection("Number", data);
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Number", provider.Key);
            Assert.Equal(ProviderType.Sequential, provider.Value.Type);
            Assert.Equal(typeof(int), provider.Value.TType);
            Assert.Equal(data, ((CollectionProvider<int>)provider.Value).Collection);
        }

        [Theory]
        [MemberData(nameof(MappingConfig.TestCases), MemberType = typeof(MappingConfig))]
        public void MappingCollectionRandomProvider(IMapping map)
        {
            List<int> data = new List<int>() { 1, 2, 3, 4 };
            var providers = map.GetFieldsWithProviders;
            var result = map.Collection("Number", data, ProviderType.Random);
            var provider = providers.FirstOrDefault();

            Assert.Equal(map, result);
            Assert.Equal("Number", provider.Key);
            Assert.Equal(ProviderType.Random, provider.Value.Type);
            Assert.Equal(typeof(int), provider.Value.TType);

            Assert.All(((CollectionProvider<int>)provider.Value).Collection, i => {
                Assert.Contains(i, data);
            });

            //Assert.Equal(data, ((CollectionProvider<int>)provider.Value).Collection);
        }
    }
}