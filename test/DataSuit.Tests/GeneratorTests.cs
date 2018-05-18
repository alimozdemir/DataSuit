using Xunit;
using Moq;
using DataSuit.Infrastructures;
using System.Linq;

namespace DataSuit.Tests
{
    public class GeneratorTests
    {
        public class TestClass
        {

        }

        [Fact]
        public void GenerateSingle()
        {
            var mockSuit = new Mock<Suit>();
            Generator<TestClass> gen = new Generator<TestClass>(mockSuit.Object);

            var result = gen.Generate();
            mockSuit.Verify(i => i.Generate<TestClass>(It.IsAny<TestClass>(), It.IsAny<ISessionManager>()));
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(1)]
        public void GenerateList(int times)
        {
            var mockSuit = new Mock<Suit>();
            Generator<TestClass> gen = new Generator<TestClass>(mockSuit.Object);

            var result = gen.Generate(count: times);
            
            mockSuit.Verify(i => 
                    i.Generate<TestClass>(It.IsAny<TestClass>(), It.IsAny<ISessionManager>()), 
                    Times.Exactly(times));

            Assert.Equal(times, result.Count());
        }
    }
}