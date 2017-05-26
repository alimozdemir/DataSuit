using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Infrastructures
{
    public class Settings : ISettings
    {
        private Dictionary<string, IDataProvider> _providers;
        public Dictionary<string, IDataProvider> Providers => _providers;

        public Settings()
        {
            _providers = new Dictionary<string, IDataProvider>();
        }

        public void AddProvider(string key, IDataProvider provider)
        {
            var keys = key.Split(',', ' ');

            foreach(var item in keys)
            {
                if (_providers.ContainsKey(item))
                    _providers[item] = provider;
                else
                    _providers.Add(item, provider);
            }
            
        }

        public void AddProvider(Dictionary<string, IDataProvider> prov)
        {
            foreach(var item in prov)
            {
                this.AddProvider(item.Key, item.Value);
            }
        }

        public bool RemoveProvider(string key)
        {
            if (_providers.ContainsKey(key))
            {
                _providers.Remove(key);
                return true;
            }

            return false;
        }

        public string Export()
        {
            throw new NotImplementedException();
        }

        public void Import(string file)
        {
            throw new NotImplementedException();
        }

    }
}
