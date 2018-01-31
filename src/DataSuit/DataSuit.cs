using System.Collections.Generic;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;

namespace DataSuit
{

    public class DataSuit
    {
        private readonly Settings _settings;
        private Dictionary<string, IDataProvider> PendingFieldsWithProviders;
        public DataSuit(Settings settings)
        {
            _settings = settings;
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

        public T Generate<T>() where T : class, new()
        {
            SetFieldsWithProviders();

            var item = new T();
            Reflection.Mapper.Map(item);
            return item;
        }

        public IEnumerable<T> Generate<T>(int count) where T : class, new()
        {
            SetFieldsWithProviders();
            
            List<T> temp = new List<T>();

            for (int i = 0; i < count; i++)
            {
                var item = new T();

                Reflection.Mapper.Map(item);

                temp.Add(item);
            }

            return temp;
        }
    }
}