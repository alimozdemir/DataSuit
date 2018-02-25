using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class JsonProvider<T> : IJsonProvider<T>
    {
        private string url;
        private JsonStatus _status = JsonStatus.NotStarted;

        private IEnumerable<T> col;
        private IEnumerator<T> iterator;

        public T Current => iterator.Current;

        object IDataProvider.Current => iterator.Current;

        public ProviderType Type => ProviderType.Json;

        public JsonStatus Status => _status;

        public Type TargetType => typeof(T);
        public Type TType => typeof(T);

        public string Url => url;

        public JsonProvider(string _url)
        {
            url = _url;
        }

        public void MoveNext(ISessionManager manager)
        {
            if (_status != JsonStatus.Ready)
                throw new Exception("Provider is not ready yet. Make sure InitializeAsync() is called.");

            if (!iterator.MoveNext())
            {
                iterator = col.GetEnumerator();
                iterator.MoveNext();
            }
        }

        public void SetData(string _url)
        {
            url = _url;
        }
        
        public async Task InitializeAsync()
        {
            if(_status == JsonStatus.NotStarted)
            {
                _status = JsonStatus.Fetching;

                //Is it well defined url
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    //Make the request
                    var response = await Utility.Client.GetAsync(url);
                    //Get the content of request
                    var content = await response.Content.ReadAsStringAsync();
                    //Deserialize Object
                    var json = JsonConvert.DeserializeObject<List<T>>(content);

                    //Check if it is valid data.
                    col = json;
                    iterator = col.GetEnumerator();
                    //First element
                    iterator.MoveNext();

                    _status = JsonStatus.Ready;
                }
                else
                {
                    _status = JsonStatus.Error;
                }
            }

        }
    }
}
