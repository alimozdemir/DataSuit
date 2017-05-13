using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DataSuit.Infrastructures
{
    internal class Utility
    {
        public static Random Rand = new Random((int)DateTime.Now.Ticks);
        public static HttpClient Client = new HttpClient();
    }
}
