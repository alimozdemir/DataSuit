using System;
using DataSuit.Enums;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class IncrementalLongProvider : IIncrementalLongProvider
    {
        public IncrementalLongProvider(string prop)
        {
            _prop = prop;
        }

        public long Current => _current;

        public ProviderType Type => ProviderType.Incremental;

        public Type TType => typeof(long);

        object IDataProvider.Current => _current;


        private long _current;
        private readonly string _prop;

        public string Prop { get { return _prop; } }
        
        public void MoveNext(ISessionManager manager)
        {
            _current = manager.IncreaseLong(_prop);
        }

    }
}