using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using Newtonsoft.Json;

namespace DataSuit.Infrastructures
{
    public class Settings : ISettings
    {
        private Dictionary<string, IDataProvider> _providers;
        private (RelationshipMap Type, int Value) _relationship;
        public Dictionary<string, IDataProvider> Providers => _providers;

        public (RelationshipMap Type, int Value)  Relationship { get => _relationship; set => _relationship = value; }

        public const int RelationshipValue = 3;
        public const RelationshipMap RelationshipType = RelationshipMap.Constant;

        public Settings()
        {
            _providers = new Dictionary<string, IDataProvider>();
            _relationship = (RelationshipType, RelationshipValue);
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


        /// <summary>
        /// Export the current settings as json serialize string
        /// </summary>
        /// <returns></returns>
        public string Export()
        {
            string result = string.Empty;
            JsonSettings settings = new JsonSettings();

            settings.RelationshipType = Common.Settings.Relationship.Type.ToString();
            settings.RelationshipValue = Common.Settings.Relationship.Value;
            List<JsonFieldSettings> providers = new List<JsonFieldSettings>();
            foreach (var item in _providers)
            {
                providers.Add(new JsonFieldSettings(item.Key, item.Value));
            }
            settings.Providers = providers;
            result = JsonConvert.SerializeObject(settings, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return result;
        }

        /// <summary>
        /// Import settings file as json serialized string
        /// </summary>
        /// <param name="file"></param>
        public void Import(string file)
        {
            var settings = JsonConvert.DeserializeObject<JsonSettings>(file);
            RelationshipMap type = (RelationshipMap)Enum.Parse(typeof(Enums.RelationshipMap), settings.RelationshipType);
            Common.Settings.Relationship = (type, settings.RelationshipValue);


            //todo
            IDataProvider provider = null;
            foreach (var item in settings.Providers)
            {

                switch (item.Type)
                {
                    case "Json":
                        break;

                    default:
                        break;
                }
            }

        }

    }
}
