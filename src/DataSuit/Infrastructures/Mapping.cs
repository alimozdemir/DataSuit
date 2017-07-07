using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataSuit.Enums;
using DataSuit.Reflection;
using System.Collections;

namespace DataSuit.Infrastructures
{
    public class Mapping : IMapping
    {
        Dictionary<string, IDataProvider> listOfFields = new Dictionary<string, IDataProvider>();

        public Dictionary<string, IDataProvider> GetFieldsWithProviders => listOfFields;

        public IMapping Set<P>(string field, P data)
        {
            StaticProvider<P> provider = new StaticProvider<P>(data);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Collection<P>(string field, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential)
        {
            CollectionProvider<P> provider = new CollectionProvider<P>(collection);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Range(string field, int min, int max)
        {
            RangeIntProvider provider = new RangeIntProvider(min, max);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Range(string field, double min, double max)
        {
            RangeDoubleProvider provider = new RangeDoubleProvider(min, max);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Phone(string field, string template)
        {
            PhoneProvider provider = new PhoneProvider(template);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Dummy(string field, int length)
        {
            DummyTextProvider provider = new DummyTextProvider(length);

            listOfFields.Add(field, provider);

            return this;
        }
    }

    public class Mapping<T> : IMapping<T> where T : class
    {
        Dictionary<string, IDataProvider> listOfFields = new Dictionary<string, IDataProvider>();

        public Dictionary<string, IDataProvider> GetFieldsWithProviders => listOfFields;

        public string GetFields => string.Join(Utility.Seperator, listOfFields.Keys);

        public IMapping<T> Collection<P>(Expression<Func<T, P>> action, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential)
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

        public IMapping<T> Range(Expression<Func<T, int>> action, int min, int max)
        {
            RangeIntProvider provider = new RangeIntProvider(min, max);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Range(Expression<Func<T, double>> action, double min, double max)
        {
            RangeDoubleProvider provider = new RangeDoubleProvider(min, max);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);
            return this;
        }
        
        public IMapping<T> Json(string url)
        {
            JsonProvider<T> provider = new JsonProvider<T>(url);

            var t = typeof(T);
            var field = t.GetAllProperties();

            listOfFields.Add(field, provider);
            return this;
        }
        
        public IMapping Set<P>(string field, P data)
        {
            StaticProvider<P> provider = new StaticProvider<P>(data);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Collection<P>(string field, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential)
        {
            CollectionProvider<P> provider = new CollectionProvider<P>(collection, type);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Range(string field, int min, int max)
        {
            RangeIntProvider provider = new RangeIntProvider(min, max);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Range(string field, double min, double max)
        {
            RangeDoubleProvider provider = new RangeDoubleProvider(min, max);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Phone<P>(Expression<Func<T, P>> action, string template)
        {
            PhoneProvider provider = new PhoneProvider(template);

            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Phone(string field, string template)
        {
            PhoneProvider provider = new PhoneProvider(template);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Dummy<P>(Expression<Func<T, P>> action, int length)
        {
            DummyTextProvider provider = new DummyTextProvider(length);

            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Dummy(string field, int length)
        {
            DummyTextProvider provider = new DummyTextProvider(length);

            listOfFields.Add(field, provider);

            return this;
        }
    }
}
