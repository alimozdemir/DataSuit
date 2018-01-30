using DataSuit.Infrastructures;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class StaticProviderTests
    {
        [Fact]
        public void SetDataWithStringValid()
        {
            StaticProvider<string> provider = new StaticProvider<string>();
            provider.SetData("Hello World!");

            Assert.Equal("Hello World!", provider.Current);
        }

        [Fact]
        public void ConstructorWithStringValid()
        {
            StaticProvider<string> provider = new StaticProvider<string>("Hello World!");

            Assert.Equal("Hello World!", provider.Current);
        }

        [Fact]
        public void ConstructorWithIntegerValid()
        {
            StaticProvider<int> provider = new StaticProvider<int>(5);

            Assert.Equal(5, provider.Current);
        }

        [Fact]
        public void SetDataWithIntegerValid()
        {
            StaticProvider<int> provider = new StaticProvider<int>();
            provider.SetData(5);

            Assert.Equal(5, provider.Current);
        }

        [Fact]
        public void SetDataWithClassValid()
        {
            TempClass temp = new TempClass();
            temp.Data = 3;

            StaticProvider<TempClass> provider = new StaticProvider<TempClass>(temp);

            Assert.Equal(temp, provider.Current);
        }

        [Fact]
        public void ConstructorWithClassValid()
        {
            TempClass temp = new TempClass();
            temp.Data = 3;

            StaticProvider<TempClass> provider = new StaticProvider<TempClass>(temp);

            Assert.Equal(temp, provider.Current);
        }

        private class TempClass
        {
            public int Data { get; set; }
        }
    }
}