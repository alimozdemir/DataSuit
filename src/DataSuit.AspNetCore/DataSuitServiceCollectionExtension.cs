using Microsoft.Extensions.DependencyInjection;

namespace DataSuit
{
    public static class DataSuitServiceCollectionExtension
    {
        public static void AddDataSuit(IServiceCollection sc)
        {
            sc.AddSingleton<DataSuit>();
        }


    }


    
}