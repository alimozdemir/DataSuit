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

        public static string[] Names()
        {
            var assembly = typeof(Generator).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceNames();
        }
    }
}
