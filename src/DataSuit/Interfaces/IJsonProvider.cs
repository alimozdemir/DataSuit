using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface IJsonProvider<T> : IDataProvider<T>
    {
        void SetData(string url);
    }
}
