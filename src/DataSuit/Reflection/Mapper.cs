using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using DataSuit.Interfaces;

namespace DataSuit.Reflection
{
    internal class Mapper
    {
        public static T Map<T>(T val)
        {
            var type = typeof(T);

            foreach(var item in type.GetTypeInfo().DeclaredProperties)
            {
                var seek = Common.Settings.Providers.FirstOrDefault(i => i.Key.Contains(item.Name));

                if (!string.IsNullOrEmpty(seek.Key))
                {
                    if (seek.Value.Type == Enums.ProviderType.Json)
                    {
                        var temp = seek.Value as IJsonProvider;
                        var targetProp = temp.TargetType.GetTypeInfo().DeclaredProperties.FirstOrDefault(i => i.Name.Equals(item.Name));
                        var value = targetProp.GetValue(seek.Value.Current);
                        item.SetValue(val, value);
                    }
                    else
                    {
                        item.SetValue(val, seek.Value.Current);
                    }
                    seek.Value.MoveNext();
                }
            }

            return val;
        }
    }
}
