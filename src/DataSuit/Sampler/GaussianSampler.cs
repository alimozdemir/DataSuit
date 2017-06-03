using System;
using System.Collections.Generic;
using DataSuit.Interfaces;

namespace DataSuit
{
    // Would be better with an IIndistinctSampler interface in between
    public class GaussianSampler<T>: IGaussianSampler<T>
    {
        public GaussianSampler()
        {

        }
        public List<T> Sample(int n)
        {
          throw new NotSupportedException();
        }
        public List<double> Sample(int n,double mean, double variance){

            Random generator = new Random();
            List<double> ret = new List<double>();

            for( int i = 0 ; i < n ; i++ )
            {
                ret.Add(Gaussian(generator.NextDouble(),generator.NextDouble(),mean,variance));
            }
            return ret;
        }
        private double Gaussian(double u1, double u2, double mean, double variance)
        {
          return variance * Math.Sqrt(-2* Math.Log(u1)) * Math.Cos(2*Math.PI*u2) + mean;
        }
    }
}
