using DataSuit.Infrastructures;
using Xunit;

namespace DataSuit.Tests
{
    public class SessionManagerTests
    {
        [Fact]
        public void OneIncreaseIntegerFromZero()
        {
            SessionManager manager = new SessionManager();
            var result = manager.IncreaseInteger("Id");

            Assert.Equal(1, result);
        }

        [Fact]
        public void TwoIncreaseIntegerFromZero()
        {
            SessionManager manager = new SessionManager();
            var result = manager.IncreaseInteger("Id");
            result = manager.IncreaseInteger("Id");
            Assert.Equal(2, result);
        }

        [InlineData(10)]
        [InlineData(20)]
        [InlineData(100)]
        [Theory]
        public void LoopIncreaseIntegerFromZero(int data)
        {
            SessionManager manager = new SessionManager();
            int result = 0;
            for(int i = 0; i < data; i++)
                result = manager.IncreaseInteger("Id");

            Assert.Equal(data, result);
        }
    }
}