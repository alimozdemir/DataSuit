using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;

namespace DataSuit.Providers
{
    public interface ICollectionProvider<T> : IDataProvider<T>
    {
        IEnumerable<T> Collection { get; }
        void SetData(IEnumerable<T> collection);
        void SetData(IEnumerable<T> collection, ProviderType providerType);
    }
}
