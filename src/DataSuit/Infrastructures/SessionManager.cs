using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DataSuit.Infrastructures
{
    public class SessionManager : ISessionManager
    {
        private readonly ConcurrentDictionary<string, int> _cdInteger;

        private readonly ConcurrentDictionary<string, long> _cdLong;

        public SessionManager() // maybe an id
        {
            _cdInteger = new ConcurrentDictionary<string, int>();
            _cdLong = new ConcurrentDictionary<string, long>();
        }

        public void Increase(string prop)
        {

        }
        public long IncreaseLong(string prop)
        {
            if (_cdLong.TryGetValue(prop, out long longValue))
            {
                Interlocked.Increment(ref longValue);
                _cdLong[prop] = longValue;

                return longValue;
            }
            else
            {
                if (!_cdLong.TryAdd(prop, 1))
                {
                    throw new Exception("IncreaseLong");
                }

                return 1;
            }
        }

        public int IncreaseInteger(string prop)
        {
            if (_cdInteger.TryGetValue(prop, out int intValue))
            {
                Interlocked.Increment(ref intValue);
                _cdInteger[prop] = intValue;

                return intValue;
            }
            else
            {
                if (!_cdInteger.TryAdd(prop, 1))
                {
                    throw new Exception("IncreaseInteger");
                }
                
                return 1;
            }
        }
    }
}