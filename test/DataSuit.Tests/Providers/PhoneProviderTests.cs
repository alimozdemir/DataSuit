using System;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class PhoneProviderTests
    {
        [Fact]
        public void EmptyFormatWithConstructor()
        {
            Action act = () => new PhoneProvider(string.Empty);

            Assert.ThrowsAny<ArgumentNullException>(act);
        }

        [Fact]
        public void InvalidFormatWithConstructor()
        {
            Action act = () => new PhoneProvider("abc*");

            Assert.ThrowsAny<ArgumentException>(act);
        }

        [Fact]
        public void ValidFormatWithIDataProvider()
        {
            var format = "(0-xxx) xxx-xx-xxx";
            IDataProvider provider = new PhoneProvider(format);

            provider.MoveNext(null);

            Assert.Equal(format.Length, ((string)provider.Current).Length);
        }


        [Fact]
        public void ValidFormatWithIDataProviderString()
        {
            var format = "(0-xxx) xxx-xx-xxx";
            IDataProvider<string> provider = new PhoneProvider(format);

            provider.MoveNext(null);
            
            Assert.Equal(format.Length, provider.Current.Length);
        }

        [Fact]
        public void ValidFormatWithPhoneProvider()
        {
            var format = "(0-xxx) xxx-xx-xxx";
            PhoneProvider provider = new PhoneProvider(format);

            provider.MoveNext(null);
            
            Assert.Equal(format.Length, provider.Current.Length);
        }

        /*[Fact]
        public void ValidFormatAsNumeric()
        {
            var format = "(0-xxx)";
            PhoneProvider provider = new PhoneProvider(format);

            provider.MoveNext(null);
            var cur = provider.Current;
            var num = provider.AsNumeric();
            Assert.Equal(3, num.ToString().Length);

        }*/
    }
}