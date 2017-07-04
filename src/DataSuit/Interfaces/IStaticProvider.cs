using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface IStaticProvider<T> : IDataProvider<T>
    {
        void SetData(T val);
        T Value { get; }
    }
}
