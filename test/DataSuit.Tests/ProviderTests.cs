using System;
using Xunit;
using DataSuit.Infrastructures;
using System.Collections.Generic;
using DataSuit.Enums;
using Xunit.Abstractions;

namespace DataSuit.Tests
{
    public class ProviderTests
    {
        ITestOutputHelper output;

        public ProviderTests(ITestOutputHelper o)
        {
            output = o;
        }
        
        class Entity
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            
            public int Age { get; set; }
        }

        class API
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        class Names
        {
            public string Name { get; set; }
            public int Gender { get; set; }
        }

        [Fact]
        public void Test1()
        {
            //Generator<Entity, API>.Set(i => i.Name, i => i.Name);
            //Assert.NotNull(DataSuit.Infrastructures.Utility.Client);

            Mapping<Entity> a = new Mapping<Entity>();
            a.Set(i => i.Age, 10, 30)
                .Set(i => i.Name, "Hi");
            /*
            Mapping<Entity> a = new Mapping<Entity>();
            a.Set(i => i.Age, new int[] { 1, 2, 3, 4, 5, 123, 1, 24, 5 }, ProviderType.Random)
                .Set(i => i.Name, "John")
                .Set(i => i.Surname, new int[] { 1, 5 }, ProviderType.Range)
                .Set(i => i.Age)
                .Set("Name,Surname", new List<string>())
                .Set<Names>(URL, Provider.API);

            Generator<Entity>.Seed(100);
            Generator<A>.Seed(100);
            Generator.Set

            



            output.WriteLine(a.Output());*/
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
