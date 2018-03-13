using System.Collections.Generic;
using System.Linq;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;
using DataSuit.Reflection;

namespace DataSuit
{

    public sealed class DataSuit
    {
        private readonly ISettings _settings;
        private readonly Mapper _mapper;
        private Dictionary<string, IDataProvider> PendingFieldsWithProviders;

        public DataSuit() : this(new Settings())
        {

        }
        public DataSuit(ISettings settings)
        {
            _settings = settings;
            _mapper = new Mapper(_settings);
        }

        public void Load()
        {
            Resources.Load(_settings);
        }

        public IMapping Build()
        {
            SetFieldsWithProviders();

            var map = new Mapping();
            PendingFieldsWithProviders = map.GetFieldsWithProviders;
            return map;
        }

        public IMapping<T> Build<T>() where T : class, new()
        {
            SetFieldsWithProviders();

            var map = new Mapping<T>();
            PendingFieldsWithProviders = map.GetFieldsWithProviders;
            return map;
        }

        private void SetFieldsWithProviders()
        {
            if (PendingFieldsWithProviders != null)
            {
                _settings.AddProvider(PendingFieldsWithProviders);
                PendingFieldsWithProviders = null;
            }
        }

        // not necessary but for know it should stay
        public void EnsureNoPendingProviders()
        {
            SetFieldsWithProviders();
        }

        public IGenerator<T> GeneratorOf<T>() where T : class, new()
        {
            return new Generator<T>(this);
        }

        public IPrimitiveGenerator GeneratorOfPrimitives()
        {
            return new PrimitiveGenerator(this);
        }

        internal void Generate<T>(T item, ISessionManager manager) where T : class, new()
        {
            SetFieldsWithProviders();

            _mapper.Map(item, manager);
        }

        internal T GeneratePrimitive<T>(string name, ISessionManager manager)
        {
            name = name.ToLower();
            
            var provider = _settings.Providers.FirstOrDefault(i => i.Key.Equals(name));

            if (string.IsNullOrEmpty(provider.Key))
                return default(T);
            else
            {
                if (typeof(T) == provider.Value.TType)
                {
                    var temp = (T)provider.Value.Current;
                    provider.Value.MoveNext(manager);
                    return temp;
                }
                else
                    return default(T);
            }
        }
    }
}