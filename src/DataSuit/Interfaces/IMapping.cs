using DataSuit.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DataSuit.Providers;

namespace DataSuit.Interfaces
{
    public interface IMapping
    {
        Dictionary<string, IDataProvider> GetFieldsWithProviders { get; }

        IMapping Set<P>(string field, P data);

        IMapping Range(string field, int min, int max);

        IMapping Range(string field, double min, double max);

        IMapping Collection<P>(string field, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential);
        IMapping Collection<P>(IEnumerable<P> collection, ProviderType type = ProviderType.Sequential) where P : class;
        IMapping Phone(string field, string template);

        IMapping Dummy(string field, int length);

        IMapping Incremental(string field);

        IMapping Guid(string field);
    }

    public interface IMapping<T> : IMapping
    {
        IMapping<T> Collection<P>(Expression<Func<T, P>> action, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential);

        IMapping<T> Set<P>(Expression<Func<T, P>> action, P data);

        IMapping<T> Range(Expression<Func<T, int>> action, int min, int max);

        IMapping<T> Range(Expression<Func<T, double>> action, double min, double max);

        IMapping<T> Phone<P>(Expression<Func<T, P>> action, string template);

        IMapping<T> Dummy<P>(Expression<Func<T, P>> action, int length);

        IMapping<T> Incremental<P>(Expression<Func<T, P>> action);

        IMapping<T> Guid(Expression<Func<T, Guid>> action);

        IMapping<T> Guid(Expression<Func<T, string>> action);

        IMapping<T> Func<P>(Expression<Func<T, P>> action, Func<P> func);

        IMapping<T> Enum<P>(Expression<Func<T, P>> action);
    }
}
