using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DataSuit.Enums;
using System.Collections;

namespace DataSuit.Infrastructures
{
    public class Mapping<T> : IMapping<T>
    {
        List<string> listOfFields = new List<string>();

        public string Output() => string.Join(",", listOfFields);

        public IMapping<T> Set<P>(Expression<Func<T, P>> action, IEnumerable<P> collection, ProviderType type = ProviderType.Sequential)
        {
            CollectionProvider<P> provider = new CollectionProvider<P>(collection, type);
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field);
            return this;
        }

        public IMapping<T> Set<P>(Expression<Func<T, P>> action, P data)
        {
            throw new NotImplementedException();
        }

        public IMapping<T> Set(Expression<Func<T, int>> action, int min, int max)
        {
            throw new NotImplementedException();
        }

        public IMapping<T> Set(Expression<Func<T, double>> action, double min, double max)
        {
            throw new NotImplementedException();
        }
    }
}
