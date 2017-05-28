using System;
using System.Collections.Generic;

namespace DataSuit.Interfaces
{
    public interface ISampler<T>
    {
        //void setData(IEnumerable<T> data);
        List<T> sample(int n);
    }
}
