using System;
using System.Collections.Generic;
using System.Linq;
using DataSuit.Enums;
using DataSuit.Infrastructures;
using DataSuit.Providers;
using Xunit;

namespace DataSuit.Tests.Providers
{
    public class CollectionProviderTests
    {

        [Fact]
        public void ConstructorWithSequentialSameHashCode()
        {
            var collection = TestIEnumarableInt();

            CollectionProvider<int> provider = new CollectionProvider<int>(collection);

            Assert.Equal(collection.GetHashCode(), provider.Collection.GetHashCode());
        }

        [Fact]
        public void ConstructorWithSequentialInteger()
        {
            var collection = TestIEnumarableInt();

            CollectionProvider<int> provider = new CollectionProvider<int>(collection);
            List<int> temp = new List<int>();

            for (int i = 0; i < collection.Count(); i++)
            {
                temp.Add(provider.Current);
                provider.MoveNext();
            }

            Assert.Equal(temp, collection);
        }

        [Fact]
        public void ConstructorWithSequentialIntegerLoop()
        {
            var collection = TestIEnumarableInt();

            CollectionProvider<int> provider = new CollectionProvider<int>(collection);
            // go over all elements, and go back to head of list
            for (int i = 0; i < collection.Count(); i++)
            {
                provider.MoveNext();
            }

            Assert.Equal(collection.First(), provider.Current);
        }

        [Fact]
        public void ConstructorWithIntegerRandom()
        {
            var collection = TestIEnumarableInt();

            CollectionProvider<int> provider = new CollectionProvider<int>(collection, ProviderType.Random);

            Assert.Contains(provider.Current, collection);
        }

        [Fact]
        public void ConstructorWithIntegerRandomHashCode()
        {
            var collection = TestIEnumarableInt();

            CollectionProvider<int> provider = new CollectionProvider<int>(collection, ProviderType.Random);

            Assert.NotEqual(collection.GetHashCode(), provider.Collection.GetHashCode());
        }

        [Fact]
        public void ConstructorCollectionNullException()
        {
            Action act = () => new CollectionProvider<int>(null);

            Assert.ThrowsAny<ArgumentNullException>(act);
        }


        [Fact]
        public void ConstructorUnsupportedProviderTypeException()
        {
            var collection = TestIEnumarableInt();
            Action act = () => new CollectionProvider<int>(collection, ProviderType.Range);

            Assert.ThrowsAny<NotSupportedException>(act);
        }

        #region Help functions

        private IEnumerable<int> TestIEnumarableInt()
        {
            return new List<int>()
            {
                1, 2, 3, 4, 5
            };
        }

        #endregion
    }
}