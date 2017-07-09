using DataSuit.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataSuit
{
    public class Resources
    {
        public static string Lorem { get; }

        static Resources()
        {
            var assembly = typeof(Generator).GetTypeInfo().Assembly;
            
            Stream loremStream = assembly.GetManifestResourceStream("DataSuit.Resources.lorem.txt");

            using (StreamReader reader = new StreamReader(loremStream))
            {
                Lorem = reader.ReadToEnd();
            }

        }

        public static void Load()
        {
            var assembly = typeof(Generator).GetTypeInfo().Assembly;

            Stream dataStream = assembly.GetManifestResourceStream("DataSuit.Resources.data.json");
            string dataString;
            using (StreamReader reader = new StreamReader(dataStream))
            {
                dataString = reader.ReadToEnd();
            }
            
            var data = JsonConvert.DeserializeObject<ResourceData>(dataString);
            //todo fake lists, retail and company.
            Common.DefaultSettings.AddProvider("App,AppName", new CollectionProvider<string>(data.AppNames,Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("FirstName", new CollectionProvider<string>(data.FirstNames, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("LastName", new CollectionProvider<string>(data.LastNames, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Adresses", new CollectionProvider<string>(data.Adresses, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Company,CompanyNames", new CollectionProvider<string>(data.CompanyNames, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Department", new CollectionProvider<string>(data.DepartmentC, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Email", new CollectionProvider<string>(data.Emails, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("IBAN", new CollectionProvider<string>(data.IBANs, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("JobTitle", new CollectionProvider<string>(data.JobTitles, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Language", new CollectionProvider<string>(data.Language, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Slogan", new CollectionProvider<string>(data.Slogans, Enums.ProviderType.Random));
            Common.DefaultSettings.AddProvider("Username", new CollectionProvider<string>(data.Usernames, Enums.ProviderType.Random));
        }

        public static string[] Names()
        {
            var assembly = typeof(Generator).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceNames();
        }
    }
}
