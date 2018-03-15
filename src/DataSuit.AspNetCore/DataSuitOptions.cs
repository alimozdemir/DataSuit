using System.Collections.Generic;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;

namespace DataSuit.AspNetCore
{
    public class DataSuitOptions
    {
        public ISettings Settings { get; internal set; }
        internal Dictionary<string, IDataProvider> Pending;
        public bool DefaultData { get; set; }
        public IMapping<T> Build<T>() where T : class, new()
        {
            var map = new Mapping<T>();

            return map;
        }
}
}