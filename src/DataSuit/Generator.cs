using System;
using System.Collections.Generic;

namespace DataSuit
{
    public class Generator<T> : IGenerator<T> where T : class, new()
    {
        private readonly DataSuit _dataSuit;
        public Generator(DataSuit dataSuit)
        {
            _dataSuit = dataSuit;
        }
        public T Generate()
        {
            T item = new T();
            _dataSuit.Generate(item);
            return item;
        }

        public IEnumerable<T> Generate(int count)
        {
            List<T> temp = new List<T>();

            for (int i = 0; i < count; i++)
            {
                var item = new T();

                _dataSuit.Generate(item);

                temp.Add(item);
            }

            return temp;
        }
    }
}