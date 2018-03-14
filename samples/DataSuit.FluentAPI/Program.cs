using System;
using System.Collections.Generic;
using DataSuit.Enums;

namespace DataSuit.FluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettings settings = new Settings();
            DataSuit suit = new DataSuit(settings);

            var barList = new List<string>() { "Foo", "Bar", "Baz" };

            suit.Build<Foo>()
                //.Collection(i => i.Bar, barList, ProviderType.Random)
                .Range(i => i.Range, 10, 40)
                .Set(i => i.Static, "DataSuit")
                .Incremental(i => i.Id)
                .Guid(i => i.IdString)
                .Func(i => i.Bar, () => Guid.NewGuid().ToString());
                
            var fooGenerator = suit.GeneratorOf<Foo>();
            var data = fooGenerator.Generate(count: 4);

            foreach (var item in data)
                Console.WriteLine($"{item.IdString} {item.Bar} {item.Range} {item.Static}");
            
        }
    }
}
