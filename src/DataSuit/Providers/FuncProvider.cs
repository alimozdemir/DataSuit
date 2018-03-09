using System;
using DataSuit.Enums;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class FuncProvider<T> : IFuncProvider<T>
    {
        private T _current;
        private Func<T> _func;

        public Func<T> DefinedFunc => _func;

        public T Current => _current;

        public ProviderType Type => ProviderType.Func;

        public Type TType => typeof(T);

        object IDataProvider.Current => _current;
        
        public FuncProvider(Func<T> func)
        {
            _func = func;
        }

        public void MoveNext(ISessionManager manager)
        {
            _current = _func();
        }
    }
}