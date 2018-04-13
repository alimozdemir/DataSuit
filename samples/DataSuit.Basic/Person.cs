using System;

namespace DataSuit.Basic
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Note { get; set; }
        public double CreditNote { get; set; }


        public override string ToString()
        {
            return $"FirstName:{FirstName}, LastName:{LastName}, Age:{Age}";
        }

        public string NoteAndCreditNote()
        {
            return $"Personal Note:{Note},CreditNote:{CreditNote}";
        }
    }
    public class Probe
    {
        public string Id { get; set; }
        //public ProbeType ProbeType { get; set; }
        public ObjectInformation Object { get; set; }
        public string Modifiers { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public ProviderType ProviderType { get; set; }

    }
    public enum ProviderType
    {
        Data1,
        Data2,
        Data3
    }
    public class ObjectInformation
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
    }
}