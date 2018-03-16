using System.Collections.Generic;
using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using DataSuit.Providers;

namespace DataSuit.AspNetCore
{
    public class DataSuitConfiguration
    {
        public DataSuitConfiguration()
        {
            Settings = new Settings();
        }

        public ISettings Settings { get; internal set; }
        internal Dictionary<string, IDataProvider> PendingFieldsWithProviders;
        /// <summary>
        /// It is enabled as default.
        /// </summary>
        /// <returns></returns>
        public bool DefaultData { get; set; } = true;

        public IMapping<T> Build<T>() where T : class, new()
        {
            var map = new Mapping<T>();
            PendingFieldsWithProviders = map.GetFieldsWithProviders;
            return map;
        }

        public IMapping Build()
        {
            var map = new Mapping();
            PendingFieldsWithProviders = map.GetFieldsWithProviders;
            return map;
        }

        private void SetFieldsWithProviders()
        {
            if (PendingFieldsWithProviders != null)
            {
                Settings.AddProvider(PendingFieldsWithProviders);
                PendingFieldsWithProviders = null;
            }
        }

        /// <summary>
        /// Making things ready.
        /// </summary>
        public void Ready()
        {
            SetFieldsWithProviders();
        }

        public string SettingsPath { get; set; }
    }

    public class DataSuitGlobalConfiguration
    {
        public static DataSuitConfiguration Configuration { get; internal set; } = new DataSuitConfiguration();
    }
}