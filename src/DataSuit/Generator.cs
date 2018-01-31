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
            return _dataSuit.Generate<T>();
        }

        public IEnumerable<T> Generate(int count)
        {
            return _dataSuit.Generate<T>(count);
        }
    }
}