using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataSuit.Enums;
using DataSuit.Reflection;

namespace DataSuit.Infrastructures
{
    public class Mapping<T> : IMapping<T> where T : class
    {
        Dictionary<string, IDataProvider> listOfFields = new Dictionary<string, IDataProvider>();

        public string Output() => string.Join(",", listOfFields.Keys);

        public IMapping<T> Set<P>(Expression<Func<T, P>> action, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential)
        {
            CollectionProvider<P> provider = new CollectionProvider<P>(collection, type);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);
            return this;
        }

        public IMapping<T> Set<P>(Expression<Func<T, P>> action, P data)
        {
            StaticProvider<P> provider = new StaticProvider<P>(data);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Set(Expression<Func<T, int>> action, int min, int max)
        {
            RangeIntProvider provider = new RangeIntProvider(min, max);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Set(Expression<Func<T, double>> action, double min, double max)
        {
            RangeDoubleProvider provider = new RangeDoubleProvider(min, max);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);
            return this;
        }
        
        public IMapping<T> Set(string url)
        {
            JsonProvider<T> provider = new JsonProvider<T>(url);

            var t = typeof(T);
            var field = t.GetAllProperties();

            listOfFields.Add(field, provider);
            return this;
        }
    }
}
