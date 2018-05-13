using Xunit;
using Moq;
using DataSuit.Infrastructures;

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
            var mockSuit = new Mock<DataSuit>();
            Generator<TestClass> gen = new Generator<TestClass>(mockSuit.Object);

            var result = gen.Generate();
            Assert.NotNull(result);
            //mockSuit.Verify(i => i.Generate<TestClass>(It.IsAny<TestClass>(), It.IsAny<ISessionManager>()));
        }
    }
}