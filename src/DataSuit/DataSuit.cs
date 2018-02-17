using System.Collections.Generic;
using System.Linq;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;

namespace DataSuit
{

    public sealed class DataSuit
    {
        private readonly ISettings _settings;
        private Dictionary<string, IDataProvider> PendingFieldsWithProviders;
        public DataSuit(ISettings settings)
        {
            _settings = settings;
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

        internal void Generate<T>(T item) where T : class, new()
        {
            SetFieldsWithProviders();

            Reflection.Mapper.Map(item, _settings);
        }

        internal T GeneratePrimitive<T>(string name)
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
                    provider.Value.MoveNext();
                    return temp;
                }
                else
                    return default(T);
            }
        }

        /*public IEnumerable<T> Primitive<T>(string name, int count)
        {
            List<T> temp = new List<T>();

            for(int i = 0; i < count; i++)
                temp.Add(GeneratePrimitive<T>(name));

            return temp;
        }*/
    }
}