using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataSuit.Enums;
using DataSuit.Reflection;
using System.Collections;
using DataSuit.Providers;
using System.Reflection;
using System.Linq;

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
            CollectionProvider<P> provider = new CollectionProvider<P>(collection, type);

            listOfFields.Add(field, provider);

            return this;
        }
        public IMapping Collection<P>(IEnumerable<P> collection, ProviderType type = ProviderType.Sequential) where P : class
        {
            CollectionProvider<P> provider = new CollectionProvider<P>(collection, type);
            var ttype = typeof(P);

            var info = ttype.GetTypeInfo();

            foreach (var prop in info.DeclaredProperties)
            {
                listOfFields.Add(prop.Name, provider);
            }

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

        public IMapping Incremental(string field)
        {
            IncrementalProvider provider = new IncrementalProvider(field);
            listOfFields.Add(field, provider);
            return this;
        }

        public IMapping Guid(string field)
        {
            FuncProvider<Guid> provider = new FuncProvider<Guid>(() => System.Guid.NewGuid());

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
        public IMapping Collection<P>(IEnumerable<P> collection, ProviderType type = ProviderType.Sequential) where P : class
        {
            CollectionProvider<P> provider = new CollectionProvider<P>(collection, type);
            var ttype = typeof(P);

            var info = ttype.GetTypeInfo();

            foreach (var prop in info.DeclaredProperties)
            {
                listOfFields.Add(prop.Name, provider);
            }

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
        public IMapping<T> Incremental<P>(Expression<Func<T, P>> action)
        {
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;
            IDataProvider provider = new IncrementalProvider(field);

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Incremental(string field)
        {
            // todo : IncrementalLongProvider
            IncrementalProvider provider = new IncrementalProvider(field);
            listOfFields.Add(field, provider);
            return this;
        }

        public IMapping<T> Guid(Expression<Func<T, Guid>> action)
        {
            FuncProvider<Guid> provider = new FuncProvider<Guid>(() => System.Guid.NewGuid());
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Guid(Expression<Func<T, string>> action)
        {
            FuncProvider<string> provider = new FuncProvider<string>(() => System.Guid.NewGuid().ToString());
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping Guid(string field)
        {
            FuncProvider<Guid> provider = new FuncProvider<Guid>(() => System.Guid.NewGuid());

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Func<P>(Expression<Func<T, P>> action, Func<P> func)
        {
            FuncProvider<P> provider = new FuncProvider<P>(func);

            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }

        public IMapping<T> Enum<P>(Expression<Func<T, P>> action)
        {
            var type = typeof(P);
            var info = type.GetTypeInfo();

            if (!info.IsEnum)
                throw new ArgumentException("Invalid enum type");

            var values = System.Enum.GetValues(type);
            var collection = values.OfType<P>().ToList();

            var provider = new CollectionProvider<P>(collection, ProviderType.Random);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field, provider);

            return this;
        }
    }
}
