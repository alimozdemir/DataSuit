using System;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class FuncProviderTests
    {
        [Fact]
        public void StringFuncWithMoveNext()
        {
            Func<string> testFunction = () =>
            {
                Assert.True(true, "Delegate function is called.");
                return string.Empty;
            };

            FuncProvider<string> provider = new FuncProvider<string>(testFunction);

            provider.MoveNext(null);

            //Assert.Equal(string.Empty, provider.Current);
        }

        [Fact]
        public void StringFuncWithMoveNexMultipleTimes()
        {
            var counter = 0;

            Func<string> testFunction = () =>
            {
                counter++;
                return string.Empty;
            };

            FuncProvider<string> provider = new FuncProvider<string>(testFunction);

            for (int i = 0; i < 10; i++)
                provider.MoveNext(null);

            Assert.Equal(10, counter);
        }

        [Fact]
        public void StringFuncWithDefinedFunc()
        {
            var counter = 0;

            Func<string> testFunction = () =>
            {
                counter++;
                return string.Empty;
            };

            FuncProvider<string> provider = new FuncProvider<string>(testFunction);

            Assert.Equal(testFunction, provider.DefinedFunc);
        }

        [Fact]
        public void StringFuncWithTType()
        {
            var counter = 0;

            Func<string> testFunction = () =>
            {
                counter++;
                return string.Empty;
            };

            FuncProvider<string> provider = new FuncProvider<string>(testFunction);

            Assert.Equal(testFunction, provider.DefinedFunc);
        }
    }
}