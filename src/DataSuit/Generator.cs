using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit
{
    public class Generator<T>
    {
        /// <summary>
        /// Global settings for generator
        /// </summary>
        private Settings settings;

        public bool AddProvider(string key, IDataProvider provider)
        {
            return settings.AddProvider(key, provider);
        }

        public bool RemoveProvider(string key, IDataProvider provider)
        {
            return settings.RemoveProvider(key);
        }


    }
}
