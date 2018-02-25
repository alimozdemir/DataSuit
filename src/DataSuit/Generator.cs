using System;
using System.Collections.Generic;
using DataSuit.Infrastructures;

namespace DataSuit
{
    public class Generator<T> : IGenerator<T> where T : class, new()
    {
        private readonly DataSuit _dataSuit;
        private readonly ISessionManager _sessionManager;
        public Generator(DataSuit dataSuit)
        {
            _dataSuit = dataSuit;
            _sessionManager = new SessionManager();
        }


        public T Generate()
        {
            T item = new T();
            _dataSuit.Generate(item, _sessionManager);
            return item;
        }

        public IEnumerable<T> Generate(int count)
        {
            List<T> temp = new List<T>();

            for (int i = 0; i < count; i++)
            {
                var item = new T();

                _dataSuit.Generate(item, _sessionManager);

                temp.Add(item);
            }

            return temp;
        }
    }
}