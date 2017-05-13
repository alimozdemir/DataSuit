using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface ISettings
    {
        Dictionary<string, IDataProvider> Providers { get; }
        bool AddProvider(string key, IDataProvider provider);
        bool RemoveProvider(string key);
    }
}
