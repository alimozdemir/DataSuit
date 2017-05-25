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

        public bool AddProvider(string key, IDataProvider provider)
        {
            //check key : "Name,Surname,LastName", "Name"
            if (!_providers.ContainsKey(key))
            {
                _providers.Add(key, provider);
                return true;
            }

            return false;
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
