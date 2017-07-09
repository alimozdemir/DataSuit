using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit
{
    internal class Common
    {
        /// <summary>
        /// Global settings for generator
        /// </summary>
        public static Settings Settings;
        internal static Settings DefaultSettings;

        static Common()
        {
            Settings = new Settings();
            DefaultSettings = new Settings();
        }

    }
    
}
