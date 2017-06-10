using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using DataSuit.Interfaces;
using System.Collections;
using DataSuit.Enums;

namespace DataSuit.Reflection
{
    internal class Mapper
    {
        public static T Map<T>(T val, RelationshipMap rel = RelationshipMap.None) where T : new()
        {
            // Usage of typeof(T) causes problems. The template class could be an object and the value could be anything
            // Therefore we can't get properties of an object type.
            var type = val.GetType();
            List<IJsonProvider> tempProviders = new List<IJsonProvider>();
            foreach(var item in type.GetTypeInfo().DeclaredProperties)
            {
                var seek = Common.Settings.Providers.FirstOrDefault(i => i.Key.Contains(item.Name));
                
                var propInfo = item.PropertyType.GetTypeInfo();
                
                if (propInfo.IsClass)
                {

                }
                else if (propInfo.IsGenericType)
                {
                    SetCollection(item, val, seek.Value);
                }
                else if (propInfo.IsPrimitive)
                {
                    if (string.IsNullOrEmpty(seek.Key))
                        continue;

                    SetPrimitive(item, val, seek.Value);
                }
                else
                {

                }
                var prop = item.PropertyType;

                var tarProp = typeof(List<>);
                
                if (rel != RelationshipMap.None && 
                    prop.IsConstructedGenericType 
                    && prop.GetGenericTypeDefinition() == tarProp.GetGenericTypeDefinition())
                {
                    var newType = item.PropertyType.GenericTypeArguments[0];
                    
                    var newTypeInfo = newType.GetTypeInfo();
                    
                    var list = item.GetValue(val) as IList;

                    int iteration = 0;
                    if(rel == RelationshipMap.Once)
                    {
                        iteration = 1;
                    }
                    for (int i = 0; i < iteration; i++)
                    {
                        var newSub = Activator.CreateInstance(newType);

                        Map(newSub);
                        list.Add(newSub);
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

        private static void SetPrimitive<T>(PropertyInfo type, T val, IDataProvider provider)
        {
            type.SetValue(val, provider.Current);
            provider.MoveNext();
        }
        
        private static void SetCollection<T>(PropertyInfo type, T val, IDataProvider provider)
        {
            var collectionType = typeof(List<>);

            //If property is List<T>
            if (collectionType.GetGenericTypeDefinition() == type.PropertyType.GetGenericTypeDefinition())
            {
                var arg = type.PropertyType.GenericTypeArguments[0];
                var argInfo = arg.GetTypeInfo();

                // Fill the class as the relationship rule possible
                if (argInfo.IsClass)
                {

                }
                else if (argInfo.IsPrimitive)
                {

                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}
