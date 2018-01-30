using DataSuit.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataSuit.Providers
{
    public interface IJsonProvider : IDataProvider
    {
        Type TargetType { get; }
        JsonStatus Status { get; }
        Task InitializeAsync();
    }
    public interface IJsonProvider<T> : IJsonProvider, IDataProvider<T>
    {
        void SetData(string url);
        string Url { get; }
    }
}
