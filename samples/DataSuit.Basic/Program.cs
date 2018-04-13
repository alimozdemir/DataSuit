using System;
using System.Collections.Generic;
using System.IO;
using DataSuit;
using Newtonsoft.Json;

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

            var personGenerator = suit.GeneratorOf<Person>();
            Console.WriteLine();
            Console.WriteLine("Generating a person.");
            var p = personGenerator.Generate();

            Console.WriteLine(p);
            Console.WriteLine();

            Console.WriteLine("Generating list of persons.");

            var listOfPersons = personGenerator.Generate(count: 10);

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

            var primitiveGenerator = suit.GeneratorOfPrimitives();

            Console.WriteLine("Generating list of names");

            var names = primitiveGenerator.String("FirstName", count: 5);
            foreach (var name in names)
                Console.WriteLine($"Name:{name}");

            Console.WriteLine("Generating list of ages");
            var ages = primitiveGenerator.Integer("age", count: 5);
            foreach (var age in ages)
                Console.WriteLine($"Age:{age}");


            List<Data> n = new List<Data>() {
                new Data() { FirstName = "John1" },
                new Data() { FirstName = "John2" },
                new Data() { FirstName = "John3" },
                new Data() { FirstName = "John4" },
                new Data() { FirstName = "John5" }};

            // set a collection of data
            var suit2 = new DataSuit();
            suit2.Build()
                .Collection(n);

            var personG = suit2.GeneratorOf<Person>();

            var personss = personG.Generate(count: 5);

            foreach (var item in personss)
                Console.WriteLine(item.FirstName);
            suit.Build<Probe>()
                .Guid(i => i.Id)
                .Set(i => i.Object.Path, "Test")
                .Enum(i => i.ProviderType);

            var gen = suit.GeneratorOf<Probe>();
            var dat = gen.Generate(count: 5);

            foreach (var item in dat)
                Console.WriteLine("dat + " + item.Id + " " + item.ProviderType);

        }
    }
}
