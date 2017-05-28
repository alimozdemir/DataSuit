using System;
using System.Collections.Generic;

namespace DataSuit.Interfaces
{
    public interface IReservoirSampler<T>: ISampler<T>
    {
        //void setData(IEnumerable<T> data);
        List<T> sample(int n);
    }
}
