using System;
using DataSuit;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;

namespace DataSuit.Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettings settings = new Settings();
            DataSuit suit = new DataSuit(settings);

            Console.WriteLine("Loading resources.");
            suit.Load();
            Console.WriteLine("Resources are loaded.");

            IGenerator<Person> personGenerator = new Generator<Person>(suit);
            Console.WriteLine();
            Console.WriteLine("Generating a person.");
            var p = personGenerator.Generate();

            Console.WriteLine(p);
            Console.WriteLine();

            Console.WriteLine("Generating list of persons.");

            var listOfPersons = personGenerator.Generate(10);

            foreach (var person in listOfPersons)
            {
                Console.WriteLine(person);
            }

            suit.Build<Person>()
                .Dummy(i => i.Note, 15)
                .Range(i => i.CreditNote, 800, 1900);
            
            Console.WriteLine("Generating a person.");
            p = personGenerator.Generate();

            Console.WriteLine(p.NoteAndCreditNote());
            Console.WriteLine();
        }
    }
}
