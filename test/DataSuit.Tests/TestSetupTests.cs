using Xunit;

namespace DataSuit.Tests
{
    public class TestSetupTests
    {
        [Fact]
        [TestSetup(typeof(TestSetupExample))]
        public void TestName()
        {
            var result = DataSuitRunner.GetSuit();
            var data = result.GeneratorOfPrimitives().String("Singer");
            Assert.Equal("Eminem", data);
        }

        public class TestSetupExample : IAttributeSuit
        {
            public TestSetupExample()
            {
                Suit = new DataSuit();

                Suit.Build()
                    .Set("Singer", "Eminem");

                Suit.EnsureNoPendingProviders();
            }
            public DataSuit Suit { get; }
        }
    }
}