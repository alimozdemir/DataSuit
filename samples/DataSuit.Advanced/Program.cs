using System;
using DataSuit;

namespace DataSuit.Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSuit suit = new DataSuit();

            suit.Load();

            var companyGenerator = suit.GeneratorOf<Company>();

            var companies = companyGenerator.Generate(count: 10);


            foreach (var item in companies)
            {
                Console.WriteLine(item.CompanyName);
                foreach (var person in item.Persons)
                    Console.WriteLine(person.FirstName + " " + person.Age);
            }

        }
    }
}
