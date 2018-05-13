using System.Collections.Generic;
using DataSuit.Infrastructures;

namespace DataSuit
{
    public class PrimitiveGenerator : IPrimitiveGenerator
    {
        private readonly Suit _suit;
        private readonly ISessionManager _sessionManager;
        public PrimitiveGenerator(Suit suit)
        {
            _suit = suit;
            _sessionManager = new SessionManager();
        }

        public double Double(string name)
        {
            return _suit.GeneratePrimitive<double>(name, _sessionManager);
        }

        public IEnumerable<double> Double(string name, int count)
        {
            List<double> temp = new List<double>();

            for (int i = 0; i < count; i++)
                temp.Add(_suit.GeneratePrimitive<double>(name, _sessionManager));

            return temp;
        }

        public int Integer(string name)
        {
            return _suit.GeneratePrimitive<int>(name, _sessionManager);
        }

        public IEnumerable<int> Integer(string name, int count)
        {
            List<int> temp = new List<int>();

            for (int i = 0; i < count; i++)
                temp.Add(_suit.GeneratePrimitive<int>(name, _sessionManager));

            return temp;
        }

        public string String(string name)
        {
            return _suit.GeneratePrimitive<string>(name, _sessionManager);
        }

        public IEnumerable<string> String(string name, int count)
        {
            List<string> temp = new List<string>();

            for (int i = 0; i < count; i++)
                temp.Add(_suit.GeneratePrimitive<string>(name, _sessionManager));

            return temp;
        }
    }
}