using DataSuit.Infrastructures;
using Moq;
using Xunit;

namespace DataSuit.Tests
{
    public class PrimitiveGeneratorTests
    {
        [Fact]
        public void GenerateIntegerSingle()
        {
            var mockSuit = new Mock<Suit>();
            var primitiveGenerator = new PrimitiveGenerator(mockSuit.Object);

            mockSuit.Setup(i => i.GeneratePrimitive<int>("test", It.IsAny<ISessionManager>()))
                .Returns(10);

            var result = primitiveGenerator.Integer("test");
            Assert.Equal(10, result);
            mockSuit.Verify(i => i.GeneratePrimitive<int>("test", It.IsAny<ISessionManager>()));
        }

        [Fact]
        public void GenerateDoubleSingle()
        {
            var mockSuit = new Mock<Suit>();
            var primitiveGenerator = new PrimitiveGenerator(mockSuit.Object);

            mockSuit.Setup(i => i.GeneratePrimitive<double>("test", It.IsAny<ISessionManager>()))
                .Returns(10d);

            var result = primitiveGenerator.Double("test");
            Assert.Equal(10d, result);
            mockSuit.Verify(i => i.GeneratePrimitive<double>("test", It.IsAny<ISessionManager>()));
        }

        [Fact]
        public void GenerateStringSingle()
        {
            var mockSuit = new Mock<Suit>();
            var primitiveGenerator = new PrimitiveGenerator(mockSuit.Object);

            mockSuit.Setup(i => i.GeneratePrimitive<string>("test", It.IsAny<ISessionManager>()))
                .Returns("Hello");

            var result = primitiveGenerator.String("test");
            Assert.Equal("Hello", result);
            mockSuit.Verify(i => i.GeneratePrimitive<string>("test", It.IsAny<ISessionManager>()));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(20)]
        public void GenerateStringList(int times)
        {
            var mockSuit = new Mock<Suit>();
            var primitiveGenerator = new PrimitiveGenerator(mockSuit.Object);

            mockSuit.Setup(i => i.GeneratePrimitive<string>("test", It.IsAny<ISessionManager>()))
                .Returns("Hello");

            var result = primitiveGenerator.String("test", count: times);

            Assert.All(result,
                i => Assert.Equal("Hello", i));

            mockSuit.Verify(i => i.GeneratePrimitive<string>("test", It.IsAny<ISessionManager>()), Times.Exactly(times));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(20)]
        public void GenerateDoubleList(int times)
        {
            var mockSuit = new Mock<Suit>();
            var primitiveGenerator = new PrimitiveGenerator(mockSuit.Object);

            mockSuit.Setup(i => i.GeneratePrimitive<double>("test", It.IsAny<ISessionManager>()))
                .Returns(10d);

            var result = primitiveGenerator.Double("test", count: times);

            Assert.All(result,
                i => Assert.Equal(10d, i));

            mockSuit.Verify(i => i.GeneratePrimitive<double>("test", It.IsAny<ISessionManager>()), Times.Exactly(times));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(20)]
        public void GenerateIntegerList(int times)
        {
            var mockSuit = new Mock<Suit>();
            var primitiveGenerator = new PrimitiveGenerator(mockSuit.Object);

            mockSuit.Setup(i => i.GeneratePrimitive<int>("test", It.IsAny<ISessionManager>()))
                .Returns(10);

            var result = primitiveGenerator.Integer("test", count: times);

            Assert.All(result,
                i => Assert.Equal(10, i));

            mockSuit.Verify(i => i.GeneratePrimitive<int>("test", It.IsAny<ISessionManager>()), Times.Exactly(times));
        }
    }
}