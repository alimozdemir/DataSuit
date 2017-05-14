using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface IMapping<T>
    {
        IMapping<T> Set<P>(Expression<Func<T, P>> action);
    }
}
