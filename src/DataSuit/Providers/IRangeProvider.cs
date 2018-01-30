using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Providers
{
    public interface IRangeProvider<T> : IDataProvider<T>
    {
        void SetData(T min, T max);
        T MinValue { get; }
        T MaxValue { get; }
    }
}
