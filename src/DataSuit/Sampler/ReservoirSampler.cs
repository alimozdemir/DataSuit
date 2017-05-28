using System;
using System.Collections.Generic;
using DataSuit.Interfaces;

namespace DataSuit
{
    public class ReservoirSampler<T>: ISampler<T>
    {
        private List<T> data;

        public ReservoirSampler(List<T> _data){
            SetData(_data);
        }

        private void SetData(List<T> _data){
            data = _data;
        }

        private int GetDataSize()
        {
            return data.Count;
        }

        public List<T> Sample(int n){
          int limit = GetDataSize();
          if( n >= limit )
          {
              return data;
          }

          List<T> ret = new List<T>();
          T[] res = new T[n];
          Random generator = new Random();

          int i = 0;
          for( ; i < n ; i++ )
          {
            res[i] = data[i];
            Console.WriteLine(res[i]);
          }

          for( ; i < limit ; i++ )
          {
            int tempIndex = generator.Next(0,i+1);
            if(tempIndex < n)
            {
              res[tempIndex] = data[i];
            }

          }

          for( i = 0 ; i < n ; i++ )
          {
              ret.Add(res[i]);
          }

          return ret;
        }
    }
}
