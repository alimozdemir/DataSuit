using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using DataSuit.Interfaces;
using System.Collections;
using DataSuit.Enums;
using DataSuit.Infrastructures;
using System.Reflection.Emit;
using DataSuit.Providers;

namespace DataSuit.Reflection
{
    internal class Mapper
    {
        private readonly ISettings _settings;

        public Mapper(ISettings settings)
        {
            _settings = settings;
        }

        public T Map<T>(T val, ISessionManager manager, bool recursion = true)
        {
            // Usage of typeof(T) causes problems. The template class could be an object and the value could be anything
            // Therefore we can't get properties of an object type.
            var type = val.GetType();

            foreach (var item in type.GetTypeInfo().DeclaredProperties)
            {
                var propInfo = item.PropertyType.GetTypeInfo();

                if (propInfo.IsPrimitive || item.PropertyType == typeof(String))
                {
                    SetPrimitive(item, val, manager);
                }
                else if (propInfo.IsGenericType)
                {
                    SetCollection(item, val, manager, recursion);
                }
                else if (propInfo.IsClass)
                {
                    SetClass(item, propInfo, val, manager);
                }
                else
                {
                    //throw new Exception($"Not supported property type {item.PropertyType}");
                }
            }

            return val;
        }

        public void SetClass<T>(PropertyInfo type, TypeInfo info, T val, ISessionManager manager)
        {
            // create a class instance
            var classInstance = Activator.CreateInstance(info);
            // set the properties of it
            Map(classInstance, manager, false);
            // set the class instance to the property
            type.SetValue(val, classInstance);
        }
        
        public void SetPrimitive<T>(PropertyInfo type, T val, ISessionManager manager)
        {
            var name = type.Name.ToLower();
            var provider = _settings.Providers.FirstOrDefault(i => i.Key.Equals(name));

            if (string.IsNullOrEmpty(provider.Key))
            {
                return;
            }

            provider.Value.MoveNext(manager);

            var value = ProviderGetValue(provider.Value, name);

            type.SetValue(val, value);
        }

        public T GetPrimitive<T>(string name, ISessionManager manager)
        {
            name = name.ToLower();
            var provider = _settings.Providers.FirstOrDefault(i => i.Key.Equals(name));

            if (string.IsNullOrEmpty(provider.Key))
            {
                return default(T);
            }

            provider.Value.MoveNext(manager);

            var value = ProviderGetValue(provider.Value, name);

            return (T)value;
        }

        private object ProviderGetValue(IDataProvider provider, string name)
        {
            if (provider.TType.IsPrimitive || provider.TType.Equals(typeof(string)))
            {
                return provider.Current;
            }
            else if (provider.TType.IsClass)
            {
                var info = provider.TType.GetTypeInfo();

                var subProp = info.DeclaredProperties.FirstOrDefault(i => i.Name.ToLower().Equals(name));
                if (subProp != null)
                {
                    return subProp.GetValue(provider.Current);
                }
            }

            throw new Exception($"Not supported T type {provider.TType}");
        }

        #region Collection set
        private void SetCollection<T>(PropertyInfo type, T val, ISessionManager manager, bool recursion = true)
        {
            var collectionType = typeof(List<>);

            //If property is List<T>
            if (collectionType.GetGenericTypeDefinition() == type.PropertyType.GetGenericTypeDefinition())
            {
                var arg = type.PropertyType.GenericTypeArguments[0];
                var argInfo = arg.GetTypeInfo();

                var collection = type.GetValue(val) as IList;

                // Fill the class as the relationship rule possible
                if (argInfo.IsPrimitive || arg == typeof(String))
                {
                    for (int i = 0; i < _settings.Relationship.Value; i++)
                    {
                        // Fill the sub instance
                        object subIns = null;

                        SetPrimitive(type.Name, ref subIns, manager);

                        collection.Add(subIns);
                    }
                }
                else if (argInfo.IsClass)
                {
                    if (recursion)
                    {
                        for (int i = 0; i < _settings.Relationship.Value; i++)
                        {
                            // Fill the sub instance
                            var subIns = Activator.CreateInstance(arg);
                            Map(subIns, manager, false);
                            collection.Add(subIns);
                        }
                    }

                }
                else
                {
                    //throw new NotSupportedException();
                }
            }
        }

        private void SetPrimitive<T>(string name, ref T val, ISessionManager manager)
        {
            var provider = _settings.Providers.FirstOrDefault(i => i.Key.Contains(name));

            if (string.IsNullOrEmpty(provider.Key))
            {
                //for now, it will change
                provider = _settings.Providers.FirstOrDefault(i => i.Key.Contains(name));

                if (string.IsNullOrEmpty(provider.Key))
                    return;
            }

            var value = ProviderGetValue(provider.Value, name);

            val = (T)value;

            provider.Value.MoveNext(manager);
        }

        #endregion
    }

}
