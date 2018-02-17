using System.Collections.Generic;

namespace DataSuit
{
    public class PrimitiveGenerator : IPrimitiveGenerator
    {
        private readonly DataSuit _suit;

        public PrimitiveGenerator(DataSuit suit)
        {
            _suit = suit;
        }

        public double Double(string name)
        {
            return _suit.GeneratePrimitive<double>(name);
        }

        public IEnumerable<double> Double(string name, int count)
        {
            List<double> temp = new List<double>();

            for (int i = 0; i < count; i++)
                temp.Add(_suit.GeneratePrimitive<double>(name));

            return temp;
        }

        public int Integer(string name)
        {
            return _suit.GeneratePrimitive<int>(name);
        }

        public IEnumerable<int> Integer(string name, int count)
        {
            List<int> temp = new List<int>();

            for (int i = 0; i < count; i++)
                temp.Add(_suit.GeneratePrimitive<int>(name));

            return temp;
        }

        public string String(string name)
        {
            return _suit.GeneratePrimitive<string>(name);
        }

        public IEnumerable<string> String(string name, int count)
        {
            List<string> temp = new List<string>();

            for (int i = 0; i < count; i++)
                temp.Add(_suit.GeneratePrimitive<string>(name));

            return temp;
        }
    }
}