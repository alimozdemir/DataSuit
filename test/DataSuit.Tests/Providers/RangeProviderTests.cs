using System;
using DataSuit.Infrastructures;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class RangeProviderTests
    {
        [Fact]
        public void ConstructorWithPositiveRange()
        {
            RangeIntProvider provider = new RangeIntProvider(5, 10);

            Assert.InRange<int>(provider.Current, 5, 10);
        }

        [Fact]
        public void ConstructorThenMoveNextWithPositiveRange()
        {
            RangeIntProvider provider = new RangeIntProvider(5, 10);

            provider.MoveNext(null);

            Assert.InRange<int>(provider.Current, 5, 10);
        }

        [Fact]
        public void ConstructorThenMoveNextWithNegativeAndPositiveRange()
        {
            RangeIntProvider provider = new RangeIntProvider(-10, 10);

            provider.MoveNext(null);

            Assert.InRange<int>(provider.Current, -10, 10);
        }


        [Fact]
        public void ConstructorWithNegativeRange()
        {
            RangeIntProvider provider = new RangeIntProvider(-10, -1);

            Assert.InRange<int>(provider.Current, -10, -1);
        }

        [Fact]
        public void ConstructorThenMoveNextWithNegativeRange()
        {
            RangeIntProvider provider = new RangeIntProvider(-10, -1);

            provider.MoveNext(null);

            Assert.InRange<int>(provider.Current, -10, -1);
        }


        [Fact]
        public void ConstructorWithInvalidRange()
        {
            Action cons = () => new RangeIntProvider(5, -10);
            Assert.ThrowsAny<ArgumentException>(cons);
        }


        [Fact]
        public void SetDataWithWithInvalidRange()
        {
            RangeIntProvider provider = new RangeIntProvider(-10, -1);

            Action cons = () => provider.SetData(5, -10);
            Assert.ThrowsAny<ArgumentException>(cons);
        }
    }
}