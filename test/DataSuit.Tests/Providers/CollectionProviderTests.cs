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
                provider.MoveNext(null);
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
                provider.MoveNext(null);
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

        [Fact]
        public void ConstructorWithSequentialClassLoop()
        {
            var collection = TestIEnumarableData();

            CollectionProvider<TestData> provider = new CollectionProvider<TestData>(collection);
            // go over all elements, and go back to head of list
            for (int i = 0; i < collection.Count(); i++)
            {
                provider.MoveNext(null);
            }

            Assert.Equal(collection.First(), provider.Current);
        }


        [Fact]
        public void ConstructorWithSequentialClass()
        {
            var collection = TestIEnumarableData();

            CollectionProvider<TestData> provider = new CollectionProvider<TestData>(collection);
            List<TestData> temp = new List<TestData>();

            for (int i = 0; i < collection.Count(); i++)
            {
                temp.Add(provider.Current);
                provider.MoveNext(null);
            }

            Assert.Equal(temp, collection);
        }
        #region Help functions

        private IEnumerable<int> TestIEnumarableInt()
        {
            return new List<int>()
            {
                1, 2, 3, 4, 5
            };
        }
        private IEnumerable<TestData> TestIEnumarableData()
        {
            return new List<TestData>()
            {
                new TestData() { FirstName = "John"},
                new TestData() { FirstName = "Dean"},
                new TestData() { FirstName = "Castiel"},
                new TestData() { FirstName = "Sam"},
            };
        }


        class TestData
        {
            public string FirstName { get; set; }
        }
        #endregion
    }
}