using DataSuit.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Providers
{

    public interface IDataProvider
    {
        object Current { get; }
        ProviderType Type { get; }

        void MoveNext();
    }

    public interface IDataProvider<T> : IDataProvider
    {
        new T Current { get; }
    }
}
