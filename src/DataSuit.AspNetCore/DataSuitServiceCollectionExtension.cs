using System;
using System.IO;
using DataSuit.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataSuit
{
    public static class DataSuitServiceCollectionExtension
    {
        public static void AddDataSuit(this IServiceCollection serviceCollection, Action<DataSuitConfiguration> options = null)
        {
            var configurationInstance = DataSuitGlobalConfiguration.Configuration;

            if (options != null)
                options(configurationInstance);

            serviceCollection.AddSingleton<Suit>(serviceProvider =>
            {
                var config = DataSuitGlobalConfiguration.Configuration;
                var suit = new Suit(config.Settings);

                // load built-in data
                if (config.DefaultData)
                    suit.Load();
                
                // if a file path is set to SettingsPath then 
                if (!string.IsNullOrEmpty(config.SettingsPath) && File.Exists(config.SettingsPath))
                {
                    suit.Import(File.ReadAllText(config.SettingsPath));
                }

                return suit;
            });

            serviceCollection.AddScoped(typeof(IGenerator<>), typeof(Generator<>));
            serviceCollection.AddScoped<IPrimitiveGenerator, PrimitiveGenerator>();
        }

    }
}