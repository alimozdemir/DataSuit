using DataSuit.Enums;
using DataSuit.Infrastructures;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class IncrementalProviderTests
    {
        [Fact]
        public void Constructor()
        {
            SessionManager manager = new SessionManager();
            IncrementalProvider provider = new IncrementalProvider("Id");

            provider.MoveNext(manager);

            Assert.Equal(1, provider.Current);
        }

        [Fact]
        public void GetNextFiveValues()
        {
            SessionManager manager = new SessionManager();
            IncrementalProvider provider = new IncrementalProvider("Id");

            for (int i = 0; i < 5; i++)
            {
                provider.MoveNext(manager);

                Assert.Equal(i + 1, provider.Current);
            }
        }

        [Fact]
        public void TypeOfProvider()
        {
            IncrementalProvider provider = new IncrementalProvider("id");

            Assert.Equal(typeof(int), provider.TType);
            Assert.Equal(ProviderType.Incremental, provider.Type);
            Assert.Equal("id", provider.Prop);
        }
    }
}