using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using Newtonsoft.Json;
using System.Linq;
using DataSuit.Providers;
using DataSuit.Infrastructures;

namespace DataSuit
{
    public class Settings : ISettings
    {
        private Dictionary<string, IDataProvider> _providers;
        private (RelationshipMap Type, int Value) _relationship;
        public Dictionary<string, IDataProvider> Providers => _providers;

        public (RelationshipMap Type, int Value) Relationship { get => _relationship; set => _relationship = value; }

        public const int RelationshipValue = 3;
        public const RelationshipMap RelationshipType = RelationshipMap.Constant;

        public Settings()
        {
            _providers = new Dictionary<string, IDataProvider>();
            _relationship = (RelationshipType, RelationshipValue);
        }

        public void AddProvider(string key, IDataProvider provider)
        {
            key = key.ToLower();
            var keys = key.Split(',', ' ');

            foreach (var item in keys)
            {
                if (_providers.ContainsKey(item))
                    _providers[item] = provider;
                else
                    _providers.Add(item, provider);
            }

        }

        public void AddProvider(Dictionary<string, IDataProvider> prov)
        {
            foreach (var item in prov)
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
            
            settings.Relationship.Type = this.Relationship.Type.ToString();
            settings.Relationship.Value = this.Relationship.Value;
            List<JsonFieldSettings> providers = new List<JsonFieldSettings>();

            foreach (var item in _providers.GroupBy(i => i.Value))
            {
                string fields = string.Join(",", item.Select(i => i.Key));
                providers.Add(new JsonFieldSettings(fields, item.Key));
            }

            settings.Providers = providers;

            result = JsonConvert.SerializeObject(settings, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return result;
        }

        /// <summary>
        /// Import settings file as json serialized string
        /// </summary>
        /// <param name="file"></param>
        public void Import(string stringData)
        {
            var settings = JsonConvert.DeserializeObject<JsonSettings>(stringData);
            RelationshipMap type = (RelationshipMap)Enum.Parse(typeof(Enums.RelationshipMap), settings.Relationship.Type);
            this.Relationship = (type, settings.Relationship.Value);

            foreach (var item in settings.Providers)
            {
                IDataProvider provider = null;
                var asEnum = Enum.Parse(typeof(ProviderType), item.Type);

                switch (asEnum)
                {
                    case ProviderType.Static:

                        var staticType = typeof(StaticProvider<>);
                        var targetType = Type.GetType(item.T, true);

                        var staticProviderWithT = staticType.MakeGenericType(targetType);

                        provider = (IDataProvider)Activator.CreateInstance(staticProviderWithT, item.Value);

                        break;
                    case ProviderType.Random:
                    case ProviderType.Sequential:

                        var collectionType = typeof(CollectionProvider<>);
                        targetType = Type.GetType(item.T, true);

                        var collectionAProviderWithT = collectionType.MakeGenericType(targetType);

                        var listType = typeof(List<>);
                        var arg1 = listType.MakeGenericType(targetType);

                        var list = JsonConvert.DeserializeObject(item.Value.ToString(), arg1);

                        provider = (IDataProvider)Activator.CreateInstance(collectionAProviderWithT, list);

                        break;
                    case ProviderType.Range:
                        targetType = Type.GetType(item.T, true);

                        if (targetType == typeof(double))
                        {
                            double min = 0, max = 0;

                            if (double.TryParse(item.MinValue.ToString(), out min) && double.TryParse(item.MaxValue.ToString(), out max))
                            {
                                provider = new RangeDoubleProvider(min, max);
                            }
                        }
                        else if (targetType == typeof(int))
                        {
                            int min = 0, max = 0;

                            if (int.TryParse(item.MinValue.ToString(), out min) && int.TryParse(item.MaxValue.ToString(), out max))
                            {
                                provider = new RangeIntProvider(min, max);
                            }
                        }

                        break;
                    case ProviderType.Phone:
                        var phoneType = typeof(PhoneProvider);

                        provider = (IDataProvider)Activator.CreateInstance(phoneType, item.Value);

                        break;
                    case ProviderType.DummyText:
                        var dummyType = typeof(DummyTextProvider);

                        //An interesting bug out of here. The item.Value is actually int32 but I think newtonsoft deserialize it to int64
                        //therefore, for now we need constructor with int64, lets investigate it in future versions.
                        provider = (IDataProvider)Activator.CreateInstance(dummyType, item.Value, TextSource.Lorem);

                        break;
                    case ProviderType.Incremental:
                        var incType = typeof(IncrementalProvider);

                        provider = (IDataProvider)Activator.CreateInstance(incType, item.Value);

                        break;
                    default:
                        break;
                }


                if (provider != null)
                {
                    _providers.Add(item.Fields, provider);
                }
                else
                    throw new ArgumentException("Unknown provider, please check your settings file.");
            }


        }

    }
}
