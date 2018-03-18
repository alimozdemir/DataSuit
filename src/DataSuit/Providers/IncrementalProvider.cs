using System;
using DataSuit.Enums;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class IncrementalProvider : IIncrementalProvider
    {
        public IncrementalProvider(string prop)
        {
            _prop = prop;
        }
        public string Prop { get { return _prop; } }
        public int Current => _current;

        public ProviderType Type => ProviderType.Incremental;

        public Type TType => typeof(int);

        object IDataProvider.Current => _current;


        private int _current;
        private readonly string _prop;

        public void MoveNext(ISessionManager manager)
        {
            _current = manager.IncreaseInteger(_prop);
        }

    }
}