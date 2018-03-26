using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using System.Text.RegularExpressions;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class PhoneProvider : IPhoneProvider
    {
        public static Regex Regex { get; set; } = new Regex("[x,0-9,+, (, ), .-]");

        private readonly string format;
        private string current = string.Empty;

        /// <summary>
        /// Generating phone number with given format
        /// </summary>
        /// <param name="_format">for example _format = 0 (xxx) xxx-xx-xx </param>
        public PhoneProvider(string _format)
        {
            if (string.IsNullOrEmpty(_format))
                throw new ArgumentNullException(nameof(_format));

            format = _format;

            if (!Regex.IsMatch(format))
            {
                throw new ArgumentException("The format of phone provider is not valid. An example one \"0 (xxx) xxx-xx-xx\" ");
            }
        }

        public string Current => current;

        public ProviderType Type => ProviderType.Phone;

        object IDataProvider.Current => current;
        public Type TType => typeof(string);

        public string Format => format;

        // to do: it should be a string, long is not appropriate for such kind of method
        public long AsNumeric()
        {
            var onlyNumbers = Regex.Replace(current, "[x,+, (, ), .-]", "");
            long result = 0;
            long.TryParse(onlyNumbers, out result);

            return result;
        }

        public void MoveNext(ISessionManager manager)
        {
            current = string.Empty;

            foreach (var item in format)
            {
                if (item == 'x')
                {
                    current += Utility.Rand.Next(0, 9);
                }
                else
                    current += item;
            }
        }

    }
}
