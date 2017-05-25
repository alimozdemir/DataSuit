using DataSuit.Infrastructures;
using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataSuit
{
    public class Generator
    {
        public static bool AddProvider(string key, IDataProvider provider)
        {
            return Common.Settings.AddProvider(key, provider);
        }
        
        public static bool RemoveProvider(string key, IDataProvider provider)
        {
            return Common.Settings.RemoveProvider(key);
        }

        public static async Task JsonAsync(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                await Utility.Client.GetAsync(url);
            }
        }
        
        public static Mapping Map()
        {
            var map = new Mapping();
            Utility.Maps.Add(map);
            return map;
        }

    }

    public class Generator<TClass> : Generator where TClass : class, new()
    {
        /// <summary>
        /// An example.
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <typeparam name="P2"></typeparam>
        /// <param name="tAction"></param>
        /// <param name="targetAction"></param>
        /*public static void Set<P, P2>(Expression<Func<TClass, P>> tAction, Expression<Func<TargetClass, P2>> targetAction)
        {
            var expression = (MemberExpression)tAction.Body;
            var targetExpression = (MemberExpression)targetAction.Body;

            var field = expression.Member.Name;
            var targetField = targetExpression.Member.Name;
        }*/

        public static TClass Seed()
        {
            var temp = new TClass();
            Reflection.Mapper.Map(temp);
            return temp;
        }
        
        public static Mapping<TClass> Map()
        {
            var map = new Mapping<TClass>();
            Utility.Maps.Add(map);
            

            return map;
        }

    }
}
