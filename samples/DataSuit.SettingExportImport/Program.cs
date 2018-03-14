using System;
using System.Collections.Generic;
using System.IO;
using DataSuit.Enums;

namespace DataSuit.SettingExportImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string>() { "John", "Dean", "Sam", "Castiel" };
            DataSuit suit = new DataSuit();
            suit.Build<Person>()
                .Incremental(i => i.Id)
                .Collection(i => i.Name, names, type: ProviderType.Random)
                .Range(i => i.Age, 20, 48)
                .Range(i => i.Salary, 4000, 10000);

            File.WriteAllText("settings.json", suit.Export());

            var loadSuit = new DataSuit();
            loadSuit.Import(File.ReadAllText("settings.json"));

            var personGenerator = loadSuit.GeneratorOf<Person>();

            var persons = personGenerator.Generate(count: 5);

            foreach (var person in persons)
            {
                Console.WriteLine($"{person.Id} {person.Name} {person.Age} {person.Salary}");
            }
        }
    }
}
