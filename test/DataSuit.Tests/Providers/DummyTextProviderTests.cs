using System;
using System.IO;
using System.Reflection;
using DataSuit.Enums;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class DummyTextProviderTests
    {
        [Fact]
        public void NegativeLengthWithConstructor()
        {
            Action act = () => new DummyTextProvider(-10);
            Assert.ThrowsAny<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ZeroLengthWithConstructor()
        {
            Action act = () => new DummyTextProvider(0);
            Assert.ThrowsAny<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void MaxLengthWithConstructor()
        {
            var assembly = typeof(DataSuitTest).GetTypeInfo().Assembly;

            Stream loremStream = assembly.GetManifestResourceStream("DataSuit.Tests.Resources.lorem.txt");
            string Lorem = string.Empty;
            using (StreamReader reader = new StreamReader(loremStream))
            {
                Lorem = reader.ReadToEnd();
            }

            var provider = new DummyTextProvider(Lorem.Length + 100);

            Assert.Equal(Lorem.Length - 1, provider.MaxLength);
            Assert.Equal(TextSource.Lorem, provider.Source);
            Assert.NotEmpty(provider.Current);
            Assert.Equal(ProviderType.DummyText, provider.Type);
        }
    }
}