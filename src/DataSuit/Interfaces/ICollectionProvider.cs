using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;

namespace DataSuit.Interfaces
{
    public interface ICollectionProvider<T> : IDataProvider<T>
    {
        void SetData(IEnumerable<T> collection);
        void SetData(IEnumerable<T> collection, ProviderType providerType);
    }
}
