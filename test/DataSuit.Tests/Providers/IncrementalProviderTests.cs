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
            IncrementalLongProvider provider = new IncrementalLongProvider("Id");

            provider.MoveNext(manager);

            Assert.Equal(1, provider.Current);
        }

        [Fact]
        public void GetNextFiveValues()
        {
            SessionManager manager = new SessionManager();
            IncrementalLongProvider provider = new IncrementalLongProvider("Id");

            for (int i = 0; i < 5; i++)
            {

                provider.MoveNext(manager);

                Assert.Equal(i + 1, provider.Current);
            }
        }
    }
}