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
        public IEnumerable<IJsonFieldSettings> Providers { get; set; } = new List<JsonFieldSettings>();
    }

    public class JsonFieldSettings : IJsonFieldSettings
    {
        public JsonFieldSettings()
        {

        }

        public JsonFieldSettings(string key, IDataProvider provider)
        {
            Fields = key;
            Type = provider.Type.ToString();

            var providerType = provider.GetType();

            switch (provider.Type)
            {
                case ProviderType.Static:
                    var info = providerType.GetTypeInfo();
                    T = info.GenericTypeArguments[0].ToString();
                    Value = provider.Current;
                    break;
                case ProviderType.Random:
                case ProviderType.Sequential:
                    info = providerType.GetTypeInfo();
                    var props = info.GetProperties();
                    var collection = props.FirstOrDefault(i => i.Name.Equals("Collection"));
                    T = info.GenericTypeArguments[0].ToString();

                    Value = collection.GetValue(provider);
                    break;
                case ProviderType.Range:
                    if (providerType == typeof(RangeIntProvider))
                    {
                        var rangeIntProvider = (RangeIntProvider)provider;
                        MinValue = rangeIntProvider.MinValue;
                        MaxValue = rangeIntProvider.MaxValue;
                        T = typeof(int).ToString();
                    }
                    else if (providerType == typeof(RangeDoubleProvider))
                    {
                        var rangeDoubleProvider = (RangeDoubleProvider)provider;
                        MinValue = rangeDoubleProvider.MinValue;
                        MaxValue = rangeDoubleProvider.MaxValue;
                        T = typeof(double).ToString();
                    }
                    break;
                case ProviderType.Json:
                    info = providerType.GetTypeInfo();
                    T = info.GenericTypeArguments[0].ToString();

                    props = info.GetProperties();
                    var url = props.FirstOrDefault(i => i.Name.Equals("Url"));

                    Value = url.GetValue(provider);
                    break;
                case ProviderType.Phone:
                    info = providerType.GetTypeInfo();
                    props = info.GetProperties();
                    var format = props.FirstOrDefault(i => i.Name.Equals("Format"));

                    Value = format.GetValue(provider);
                    break;
                case ProviderType.DummyText:
                    info = providerType.GetTypeInfo();
                    props = info.GetProperties();
                    var maxLen = props.FirstOrDefault(i => i.Name.Equals("MaxLength"));

                    Value = maxLen.GetValue(provider);

                    break;
                default:
                    break;
            }


        }


        public string Fields { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public string T { get; set; }
    }
}
