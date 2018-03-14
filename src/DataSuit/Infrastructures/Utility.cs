using DataSuit.Interfaces;
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
        public static readonly string Seperator = ",";
    }
}
