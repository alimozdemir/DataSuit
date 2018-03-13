using System.Collections.Generic;

namespace DataSuit.Advanced
{
    public class Company
    {
        public Company()
        {
            Persons = new List<Person>();
        }
        public string CompanyName { get; set; }
        public List<Person> Persons { get; set; }
    }
}