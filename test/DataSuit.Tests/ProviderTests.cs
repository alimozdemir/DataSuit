using System;
using Xunit;
using DataSuit.Infrastructures;
using System.Collections.Generic;
using DataSuit.Enums;
using Xunit.Abstractions;
using System.Reflection;
using System.Linq;
using DataSuit.Interfaces;
using System.Threading.Tasks;

namespace DataSuit.Tests
{
    public class ProviderTests
    {
        ITestOutputHelper output;

        public ProviderTests(ITestOutputHelper o)
        {
            output = o;
        }
        

        class JsonProviderClass
        {
            public string Name { get; set; }
            public int Gender { get; set; }
        }


        class JsonData
        {
            public string Name { get; set; }
            public int Gender { get; set; }
        }


        class CollectionClass
        {
            public List<string> Name { get; set; } = new List<string>();
            public List<int> Gender { get; set; } = new List<int>();
        }

        [Fact]
        public async Task CollectionProperties()
        {
            Generator<JsonData>.Map()
                .Set("http://datasuit.yazilimda.com/api/Filter/FirstName/5");

            var test = await Generator<CollectionClass>.SeedAsync(5);

            foreach (var item in test)
            {
                Assert.True(item.Name.Count > 0);
                Assert.True(item.Gender.Count > 0);
            }
        }

        [Fact]
        public async Task JsonProvider()
        {
            Generator<JsonData>.Map()
                .Set("http://datasuit.yazilimda.com/api/Filter/FirstName/5");

            var test = await Generator<JsonProviderClass>.SeedAsync(5);

            foreach (var item in test)
            {
                Assert.NotEqual(item.Name, string.Empty);
                Assert.InRange(item.Gender, 0, 1);
            }
        }

        [Fact]
        public void StaticProvider()
        {
            var provider = new StaticProvider<string>("Hello World!");
            string hello = "Hello World!";
            Assert.Equal(provider.Current, hello);
        }

        [Fact]
        public void CollectionProvider()
        {
            var list = new List<int>() { 3, 4 };
            var provider = new CollectionProvider<int>(list);
            Assert.Equal(provider.Type, ProviderType.Sequential);

            Assert.Equal(list[0], provider.Current);
            provider.MoveNext();
            Assert.Equal(list[1], provider.Current);
            provider.MoveNext();
            Assert.Equal(list[0], provider.Current);
            provider.MoveNext();
            Assert.Equal(list[1], provider.Current);
        }
        [Fact]
        public void CollectionRandomProvider()
        {
            var list = new List<int>() { 3, 4 };
            var provider = new CollectionProvider<int>(list);
            provider.SetData(list, ProviderType.Random);

            Assert.Equal(provider.Type, ProviderType.Random);

            Assert.True(list.Contains(provider.Current));
            provider.MoveNext();
            Assert.True(list.Contains(provider.Current));

        }

        [Fact]
        public void RangeIntProvider()
        {
            int min = 10, max = 20;
            var provider = new RangeIntProvider(min, max);

            for (int i = 0; i < 5; i++)
            {
                Assert.True(provider.Current >= min && provider.Current <= max, $"Value : {provider.Current}");
                provider.MoveNext();
            }
        }
        [Fact]
        public void RangeDoubleProvider()
        {
            int min = 10, max = 20;
            var provider = new RangeDoubleProvider(min, max);

            for (int i = 0; i < 5; i++)
            {
                Assert.True(provider.Current >= min && provider.Current <= max, $"Value : {provider.Current}");
                provider.MoveNext();
            }
        }
    }
}
