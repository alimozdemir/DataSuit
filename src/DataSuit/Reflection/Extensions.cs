using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;


namespace DataSuit.Reflection
{
    public static class Extensions
    {
        public static string GetAllProperties(this Type val)
        {
            return string.Join(",", val.GetTypeInfo().DeclaredProperties.Select(i => i.Name)); 
        }
    }
}
