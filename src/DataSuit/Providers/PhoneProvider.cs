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
            format = _format;

            if (!Regex.IsMatch(format))
            {
                throw new ArgumentException("The format of phone provider is not valid. An example one \"0 (xxx) xxx-xx-xx\" ");
            }

            MoveNext();
        }

        public string Current => current;

        public ProviderType Type => ProviderType.Phone;

        object IDataProvider.Current => current;

        public string Format => format;

        public long AsNumeric()
        {
            var onlyNumbers = Regex.Replace(current, "[x,+, (, ), .-]", "");
            long result = 0;
            long.TryParse(onlyNumbers, out result);

            return result;
        }

        public void MoveNext()
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
