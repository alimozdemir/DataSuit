using System;
using System.Collections.Generic;
using DataSuit.Interfaces;

namespace DataSuit
{
    // Would be better with an IIndistinctSampler interface in between
    public class IndistinctSampler<T>: ISampler<T>
    {
        private List<T> data;

        public IndistinctSampler(List<T> _data)
        {
            SetData(_data);
        }

        private void SetData(List<T> _data)
        {
            data = _data;
        }

        private int GetDataSize()
        {
            return data.Count;
        }

        public List<T> Sample(int n){

            List<T> ret = new List<T>();
            Random generator = new Random();

            for(int i = 0 ; i < n ; i++)
            {
                ret.Add(data[generator.Next(0,GetDataSize())]);
            }
            return ret;
        }
    }
}
