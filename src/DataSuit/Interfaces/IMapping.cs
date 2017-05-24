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
        IMapping Set(string fields, IEnumerable list);
    }
    public interface IMapping<T>
    {
        /// <summary>
        /// Set a collection provider
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="action"></param>
        /// <param name="collection"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IMapping<T> Set<P>(Expression<Func<T, P>> action, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential);
        /// <summary>
        /// Set a static provider
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="action"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        IMapping<T> Set<P>(Expression<Func<T, P>> action, P data);

        /// <summary>
        /// Set a integer range provider.
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="action"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        IMapping<T> Set(Expression<Func<T, int>> action, int min, int max);

        /// <summary>
        /// Set a double range provider.
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="action"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        IMapping<T> Set(Expression<Func<T, double>> action, double min, double max);


        /// <summary>
        /// Set a json provider
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IMapping<T> Set(string url);
    }
}
