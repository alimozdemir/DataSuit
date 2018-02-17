using System.Collections.Generic;

namespace DataSuit
{
    public interface IPrimitiveGenerator
    {
        string String(string name);
        IEnumerable<string> String(string name, int count);

        int Integer(string name);
        IEnumerable<int> Integer(string name, int count);

        double Double(string name);
        IEnumerable<double> Double(string name, int count);
    }
}