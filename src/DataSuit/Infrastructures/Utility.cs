﻿using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DataSuit.Infrastructures
{
    internal class Utility
    {
        public static Random Rand = new Random((int)DateTime.Now.Ticks);
        public static HttpClient Client;
        public static readonly string Seperator = ",";
        public static List<IMapping> Maps { get; set; } = new List<IMapping>();
        public static List<IMapping> PendingMaps { get; set; } = new List<IMapping>();
        
        static Utility()
        {
            var ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
            Client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
            Client.DefaultRequestHeaders.Add("User-Agent", ua);
        }
    }
}
