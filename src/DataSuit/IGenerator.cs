using System.Collections.Generic;

namespace DataSuit
{
    public interface IGenerator<T> where T : class, new()
    {
        T Generate();
        IEnumerable<T> Generate(int count);
    }
}