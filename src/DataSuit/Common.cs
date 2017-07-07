using DataSuit.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit
{
    public class Common
    {
        /// <summary>
        /// Global settings for generator
        /// </summary>
        public static Settings Settings;
        static Common()
        {
            Settings = new Settings();
        }

    }
    
}
