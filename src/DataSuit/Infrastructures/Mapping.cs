using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataSuit.Infrastructures
{
    public class Mapping<T> : IMapping<T>
    {
        List<string> listOfFields = new List<string>();
        public IMapping<T> Set<P>(Expression<Func<T, P>> action)
        {
            var expression = (MemberExpression)action.Body;
            var field = expression.Member.Name;

            listOfFields.Add(field);
            return this;
        }

        public string Output() => string.Join(",", listOfFields);
    }
}
