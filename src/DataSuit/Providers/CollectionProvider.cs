using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using System.Linq;

namespace DataSuit.Providers
{
    public class CollectionProvider<T> : ICollectionProvider<T>
    {
        private IEnumerable<T> col;
        private IEnumerator<T> iterator;
        private ProviderType type;

        public T Current => iterator.Current;

        public ProviderType Type => type;

        object IDataProvider.Current => iterator.Current;

        public IEnumerable<T> Collection => col;

        public Type TType => typeof(T);

        public CollectionProvider(IEnumerable<T> collection) 
                    : this(collection, ProviderType.Sequential)
        {
        }

        public CollectionProvider(IEnumerable<T> collection, ProviderType providerType)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            SetData(collection, providerType);
        }

        public void MoveNext()
        {
            if (!iterator.MoveNext())
            {
                iterator = col.GetEnumerator();
                iterator.MoveNext();
            }
        }

        public void SetData(IEnumerable<T> collection)
        {
            col = collection;
        }

        public void SetData(IEnumerable<T> collection, ProviderType providerType)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (providerType == ProviderType.Sequential)
            {
                col = collection;

                iterator = col.GetEnumerator();
                //To get first element on collection.
                iterator.MoveNext();

                type = providerType;
            }
            else if (providerType == ProviderType.Random)
            {
                col = collection.OrderByDescending(i => Guid.NewGuid());
                
                iterator = col.GetEnumerator();
                //To get first element on collection.
                iterator.MoveNext();
                type = providerType;
            }
            else
                throw new NotSupportedException($"{providerType} does not supported on CollectionProvider");
        }
    }
}
