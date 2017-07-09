using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;

namespace DataSuit.Infrastructures
{
    public class StaticProvider<T> : IStaticProvider<T>
    {
        private T staticData;

        public T Current => staticData;

        public ProviderType Type => ProviderType.Static;

        object IDataProvider.Current => staticData;

        public T Value => staticData;

        public StaticProvider(T val)
        {
            staticData = val;
        }

        /// <summary>
        /// Empty method
        /// </summary>
        public void MoveNext()
        {
            
        }

        public void SetData(T val)
        {
            staticData = val;
        }
    }
}
