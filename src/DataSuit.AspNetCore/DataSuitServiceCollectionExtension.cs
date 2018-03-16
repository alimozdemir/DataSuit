using System;
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
                
            serviceCollection.AddSingleton<DataSuit>(serviceProvider =>
            {
                var suit = new DataSuit(DataSuitGlobalConfiguration.Configuration.Settings);

                if (DataSuitGlobalConfiguration.Configuration.DefaultData)
                    suit.Load();

                return suit;
            });

            serviceCollection.AddScoped(typeof(IGenerator<>), typeof(Generator<>));
        }


    }
}