using Xunit;

namespace DataSuit.Tests
{
    public class TestSetupTests
    {
        [Fact]
        [TestSetup(typeof(TestSetupExample))]
        public void Example()
        {
            var suit = DataSuitRunner.GetSuit();
            var data = suit.GeneratorOfPrimitives().String("Singer");
            Assert.Equal("Eminem", data);
        }

        public class TestSetupExample : IAttributeSuit
        {
            public TestSetupExample()
            {
                Suit = new Suit();

                Suit.Build()
                    .Set("Singer", "Eminem");

                Suit.EnsureNoPendingProviders();
            }
            public Suit Suit { get; }
        }
    }
}