using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;

namespace DataSuit.Infrastructures
{
    public class JsonProvider<T> : IJsonProvider<T>
    {
        private string Url;
        public T Current => throw new NotImplementedException();

        object IDataProvider.Current => throw new NotImplementedException();

        public ProviderType Type => ProviderType.Json;

        public JsonProvider(string url)
        {
            Url = url;
        }

        public void MoveNext()
        {
            throw new NotImplementedException();
        }

        public void SetData(string url)
        {
            Url = url;
        }
        
    }
}
