using DataSuit.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Interfaces
{
    public interface IJsonSettings
    {
        Relationship Relationship { get; set; }
        IEnumerable<IJsonFieldSettings> Providers { get; set; }
    }

    public interface IJsonFieldSettings
    {
        string Fields { get; set; }
        string Type { get; set; }
        object Value { get; set; }
        object MinValue { get; set; }
        object MaxValue { get; set; }
        string T { get; set; }
    }

    public class Relationship
    {
        public string Type { get; set; }
        public int Value { get; set; }
    }
}
