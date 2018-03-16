using DataSuit.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DataSuit.Providers;
using DataSuit.Interfaces;

namespace DataSuit
{
    internal class Resources
    {
        public static string Lorem { get; }

        static Resources()
        {
            var assembly = typeof(Generator<>).GetTypeInfo().Assembly;

            Stream loremStream = assembly.GetManifestResourceStream("DataSuit.Resources.lorem.txt");

            using (StreamReader reader = new StreamReader(loremStream))
            {
                Lorem = reader.ReadToEnd();
            }

        }

        public static void Load(ISettings settings)
        {
            var assembly = typeof(Generator<>).GetTypeInfo().Assembly;

            Stream dataStream = assembly.GetManifestResourceStream("DataSuit.Resources.data.json");
            string dataString;
            using (StreamReader reader = new StreamReader(dataStream))
            {
                dataString = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<ResourceData>(dataString);
            //todo fake lists, retail and company.
            settings.AddProvider("App,AppName", new CollectionProvider<string>(data.AppNames, Enums.ProviderType.Random));
            settings.AddProvider("FirstName,Name", new CollectionProvider<string>(data.FirstNames, Enums.ProviderType.Random));
            settings.AddProvider("LastName,Surname", new CollectionProvider<string>(data.LastNames, Enums.ProviderType.Random));
            settings.AddProvider("Address", new CollectionProvider<string>(data.Addresses, Enums.ProviderType.Random));
            settings.AddProvider("Company,CompanyName", new CollectionProvider<string>(data.CompanyNames, Enums.ProviderType.Random));
            settings.AddProvider("Department", new CollectionProvider<string>(data.DepartmentC, Enums.ProviderType.Random));
            settings.AddProvider("Email", new CollectionProvider<string>(data.Emails, Enums.ProviderType.Random));
            settings.AddProvider("IBAN", new CollectionProvider<string>(data.IBANs, Enums.ProviderType.Random));
            settings.AddProvider("JobTitle", new CollectionProvider<string>(data.JobTitles, Enums.ProviderType.Random));
            settings.AddProvider("Language", new CollectionProvider<string>(data.Language, Enums.ProviderType.Random));
            settings.AddProvider("Slogan", new CollectionProvider<string>(data.Slogans, Enums.ProviderType.Random));
            settings.AddProvider("Username", new CollectionProvider<string>(data.Usernames, Enums.ProviderType.Random));

            settings.AddProvider("Age", new RangeIntProvider(1, 80));
        }

        public static string[] Names()
        {
            var assembly = typeof(Generator<>).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceNames();
        }
    }
}
