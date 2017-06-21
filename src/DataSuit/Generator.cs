using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataSuit
{
    public class Generator
    {
        public static void AddProvider(string key, IDataProvider provider)
        {
            Common.Settings.AddProvider(key, provider);
        }

        public static void AddProvider(Dictionary<string, IDataProvider> prov)
        {
            Common.Settings.AddProvider(prov);
        }

        public static bool RemoveProvider(string key, IDataProvider provider)
        {
            return Common.Settings.RemoveProvider(key);
        }

        public static async Task JsonAsync(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                await Utility.Client.GetAsync(url);
            }
        }
        
        public static Mapping Map()
        {
            var map = new Mapping();

            Utility.PendingMaps.Add(map);

            return map;
        }

        protected static void CheckMaps()
        {
            foreach(var item in Utility.PendingMaps)
            {
                AddProvider(item.GetFieldsWithProviders);
                Utility.Maps.Add(item);
            }

            Utility.PendingMaps.Clear();
        }

        protected async static Task JsonProviderInitialize()
        {
            var providers = Common.Settings.Providers.Values.Where(i => i.Type == Enums.ProviderType.Json).ToList();
            
            foreach(var item in providers)
            {
                var temp = item as IJsonProvider;
                if (temp.Status == Enums.JsonStatus.NotStarted)
                {
                    await temp.InitializeAsync();
                }
            }
        }

    }

    public class Generator<TClass> : Generator where TClass : class, new()
    {
        public static TClass Seed()
        {
            var temp = new TClass();

            CheckMaps();

            Reflection.Mapper.Map(temp);

            return temp;
        }

        public static IEnumerable<TClass> Seed(int count, Enums.RelationshipMap Type = Settings.RelationshipType, int Value = Settings.RelationshipValue)
        {
            List<TClass> temp = new List<TClass>();
            CheckMaps();

            for(int i = 0; i < count; i++)
            {
                var item = new TClass();
                
                Reflection.Mapper.Map(item);

                temp.Add(item);
            }

            return temp;
        }

        public static async Task<TClass> SeedAsync(Enums.RelationshipMap Type = Settings.RelationshipType, int Value = Settings.RelationshipValue)
        {
            var temp = new TClass();

            CheckMaps();

            await JsonProviderInitialize();

            Reflection.Mapper.Map(temp);

            return temp;
        }
        
        public static async Task<IEnumerable<TClass>> SeedAsync(int count, Enums.RelationshipMap Type = Settings.RelationshipType, int Value = Settings.RelationshipValue)
        {
            List<TClass> temp = new List<TClass>();
            CheckMaps();
            await JsonProviderInitialize();

            for (int i = 0; i < count; i++)
            {
                var item = new TClass();

                Reflection.Mapper.Map(item);

                temp.Add(item);
            }

            return temp;
        }


        public static new Mapping<TClass> Map()
        {
            var map = new Mapping<TClass>();

            Utility.PendingMaps.Add(map);

            return map;
        }

    }
}
