using System;
using System.Collections.Generic;

namespace DataSuit.Interfaces
{
    public interface ISampler<T>
    {
        //void setData(IEnumerable<T> data);
        List<T> Sample(int n);
    }

    public interface IGaussianSampler<T> : ISampler<T>
    {
        List<double> Sample(int n, double mean, double variance);
    }
}
