using DataSuit.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface IMapping
    {
        Dictionary<string, IDataProvider> GetFieldsWithProviders { get; }

        IMapping Set<P>(string field, P data);

        IMapping Set<P>(string field, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential);
    }

    public interface IMapping<T> : IMapping
    {
        IMapping<T> Set<P>(Expression<Func<T, P>> action, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential);

        IMapping<T> Set<P>(Expression<Func<T, P>> action, P data);
        
        IMapping<T> Set(Expression<Func<T, int>> action, int min, int max);
        
        IMapping<T> Set(Expression<Func<T, double>> action, double min, double max);
        
        IMapping<T> Set(string url);
    }
}
