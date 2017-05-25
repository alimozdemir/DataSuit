using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

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
                    item.SetValue(val, seek.Value.Current);
                    seek.Value.MoveNext();
                }
            }

            return val;
        }
    }
}
