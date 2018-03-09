using System;

namespace DataSuit.Providers
{
    public interface IFuncProvider<T> : IDataProvider<T>
    {
         Func<T> DefinedFunc {get;}
    }
}