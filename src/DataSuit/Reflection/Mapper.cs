using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using DataSuit.Interfaces;
using System.Collections;

namespace DataSuit.Reflection
{
    internal class Mapper
    {
        public static T Map<T>(T val)
        {
            var type = typeof(T);
            List<IJsonProvider> tempProviders = new List<IJsonProvider>();
            foreach(var item in type.GetTypeInfo().DeclaredProperties)
            {
                var seek = Common.Settings.Providers.FirstOrDefault(i => i.Key.Contains(item.Name));

                var prop = item.PropertyType;
                var tarProp = typeof(List<>);
                if (prop.IsConstructedGenericType)
                {
                    var test1 = prop.GetGenericTypeDefinition();
                    var test2 = tarProp.GetGenericTypeDefinition();
                }
                if (prop.IsConstructedGenericType && prop.GetGenericTypeDefinition() == tarProp.GetGenericTypeDefinition())
                {
                    var newType = item.PropertyType.GenericTypeArguments[0];
                    var list = item.GetValue(val) as IList;
                    //todo
                    foreach (var item2 in list)
                    {
                        Map(item2);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(seek.Key))
                    {
                        if (seek.Value.Type == Enums.ProviderType.Json)
                        {
                            var temp = seek.Value as IJsonProvider;
                            var targetProp = temp.TargetType.GetTypeInfo().DeclaredProperties.FirstOrDefault(i => i.Name.Equals(item.Name));
                            var value = targetProp.GetValue(seek.Value.Current);
                            item.SetValue(val, value);
                            tempProviders.Add(temp);
                        }
                        else
                        {
                            item.SetValue(val, seek.Value.Current);
                            seek.Value.MoveNext();
                        }
                    }
                }
            }

            foreach (var item in tempProviders)
            {
                item.MoveNext();
            }

            return val;
        }
    }
}
