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
using System.IO;
using System.Diagnostics;

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


        public class JsonData
        {
            public JsonData()
            {

            }
            public string Name { get; set; }
            public int Gender { get; set; }
        }


        class CollectionClass
        {
            public List<string> Name { get; set; } = new List<string>();
            public List<int> Gender { get; set; } = new List<int>();
        }

        class ExampleClass
        {
            public string FirstName { get; set; }
            public string JobTitle { get; set; }
            public string CompanyName { get; set; }
            public string Department { get; set; }
            public string Email { get; set; }
        }


        [Fact]
        public void LoadData()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            Resources.Load();
            sw.Stop();



            output.WriteLine(sw.Elapsed.ToString());

            var ex = Generator<ExampleClass>.Seed();

            output.WriteLine($"{ex.FirstName} {ex.Email} {ex.CompanyName} {ex.Department} {ex.JobTitle}");
        } 


        [Fact]
        public void ExportSettings()
        {
            Generator.ClearSettings();

            //Collection and range
            Generator.Map().Collection("Presidents", new List<string>() { "John", "Kenndy" }, ProviderType.Random)
                .Range("Age", 10, 30)
                .Dummy("Description", 30);

            //json
            Generator<JsonData>.Map()
                .Json("http://datasuit.yazilimda.com/api/Filter/FirstName/5");
            
            //Static and phone
            Generator.Map().Set("HelloMessage", "Hello World !").Phone("Phone", "(545) xxx-xx-xx");
            
            Generator.SaveSettings("save.json");

            Assert.True(File.Exists("save.json"));
        }

        [Fact]
        public void ClearSettings()
        {
            Generator.ClearSettings();

            Assert.Equal(Generator.AllProviderNames().Length, 0);
        }

        [Fact]
        public void ImportSettings()
        {
            Generator.Register(typeof(JsonData).GetTypeInfo().Assembly);
            Generator.ClearSettings();
            Generator.LoadSettings("save.json");

            Generator.SaveSettings("saveTemp.json");

        }

        [Fact]
        public void DummyTextProvider()
        {
            DummyTextProvider provider = new DummyTextProvider(25);
            Assert.InRange(provider.Current.Length, 1, 25);
        }

        [Fact]
        public void PhoneProvider()
        {
            PhoneProvider provider = new PhoneProvider("(545) xxx-xx-xx");

            Assert.Equal("(545) xxx-xx-xx".Length, provider.Current.Length);
            Assert.False(provider.Current.Contains('x'));
            Assert.True(provider.AsNumeric() > 0);
        }

        [Fact]
        public async Task CollectionProperties()
        {
            Generator<JsonData>.Map()
                .Json("http://datasuit.yazilimda.com/api/Filter/FirstName/5");

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
                .Json("http://datasuit.yazilimda.com/api/Filter/FirstName/5");

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
