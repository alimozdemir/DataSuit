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
        
        class Entity
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            
            public int Age { get; set; }
            public override string ToString()
            {
                return $"Name {this.Name} Age {this.Age} Surname {this.Surname }";
            }
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
        public async Task Test1()
        {
            /*Generator<Entity>.Map()
                .Set(i => i.Age, 10, 30)
                .Set(i => i.Name, "Alim");
                */
            //Generator.Map()
                //.Set<string>("Surname", new List<string>() { "Özdemir", "Aydemir" });

            Generator<Names>.Map()
                .Set("http://datasuit.yazilimda.com/api/Filter/FirstName/5/0");

            //var anEntity = Generator<Entity>.Seed();
            var test = await Generator<Names>.SeedAsync();

            var data = Common.Settings.Providers.FirstOrDefault(i => i.Value.Type == ProviderType.Json);
            var js = data.Value as IJsonProvider;


            //output.WriteLine(anEntity.ToString());
            output.WriteLine(js.Status.ToString() + " " + test.Name);
            //output.WriteLine(string.Join(",", DataSuit.Common.Settings.Providers.Keys));
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
