using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface ISettings
    {
        (Enums.RelationshipMap Type, int Value) Relationship { get; set; }
        Dictionary<string, IDataProvider> Providers { get; }
        void AddProvider(string key, IDataProvider provider);
        void AddProvider(Dictionary<string, IDataProvider> prov);
        bool RemoveProvider(string key);
        string Export();
        void Import(string file);

    }
}
