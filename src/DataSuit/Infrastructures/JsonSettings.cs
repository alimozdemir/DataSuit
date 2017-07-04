using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using DataSuit.Enums;

namespace DataSuit.Infrastructures
{
    public class JsonSettings : IJsonSettings
    {
        public string RelationshipType { get; set; }
        public int RelationshipValue { get; set; }
        public IEnumerable<IJsonFieldSettings> Providers { get; set; }
    }

    public class JsonFieldSettings : IJsonFieldSettings
    {
        public JsonFieldSettings(string key, IDataProvider provider)
        {
            Fields = key;
            Type = provider.Type.ToString();

            var providerType = provider.GetType();

            if (provider.Type == Enums.ProviderType.Range)
            {
                if(providerType == typeof(RangeIntProvider))
                {
                    var rangeIntProvider = (RangeIntProvider)provider;
                    MinValue = rangeIntProvider.MinValue;
                    MaxValue = rangeIntProvider.MaxValue;
                }
                else if(providerType == typeof(RangeDoubleProvider))
                {
                    var rangeDoubleProvider = (RangeDoubleProvider)provider;
                    MinValue = rangeDoubleProvider.MinValue;
                    MaxValue = rangeDoubleProvider.MaxValue;
                }
            }
            else if(provider.Type == Enums.ProviderType.Static)
            {
                Value = provider.Current;
            }
            else if(provider.Type == Enums.ProviderType.Sequential || provider.Type == Enums.ProviderType.Random)
            {
                var props = providerType.GetTypeInfo().GetProperties();
                var collection = props.FirstOrDefault(i => i.Name.Equals("Collection"));

                Value = collection.GetValue(provider);
            }
            else if(provider.Type == Enums.ProviderType.Json)
            {
                //todo
            }

        }


        public string Fields { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
    }
}
