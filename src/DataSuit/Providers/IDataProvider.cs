using DataSuit.Enums;
using DataSuit.Infrastructures;
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
        Type TType { get; }
        void MoveNext(ISessionManager manager);
    }

    public interface IDataProvider<T> : IDataProvider
    {
        new T Current { get; }
    }
}
