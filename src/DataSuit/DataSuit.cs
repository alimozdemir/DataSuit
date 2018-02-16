using System.Collections.Generic;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;

namespace DataSuit
{

    public sealed class DataSuit
    {
        private readonly Settings _settings;
        private Dictionary<string, IDataProvider> PendingFieldsWithProviders;
        public DataSuit(Settings settings)
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

        public Dictionary<string, IDataProvider> Test()
        {
            return PendingFieldsWithProviders;
        }

        internal void Generate<T>(T item) where T : class, new()
        {
            Reflection.Mapper.Map(item, _settings);
        }

    }
}