using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class DummyTextProvider : IDummyTextProvider
    {
        private readonly int maxLength = 0;
        private readonly TextSource source = TextSource.Lorem;
        private string current;
        public Type TType => typeof(string);

        public DummyTextProvider(int _maxLength, TextSource _source = TextSource.Lorem)
        {
            if (_maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_maxLength));
            }

            if (_maxLength > Resources.Lorem.Length)
                _maxLength = Resources.Lorem.Length - 1;

            maxLength = _maxLength;
            source = _source;

            int lastSpace = Resources.Lorem.LastIndexOf(' ', maxLength);
            current = Resources.Lorem.Substring(0, lastSpace);
        }

        public int MaxLength => maxLength;

        public TextSource Source => source;

        public string Current => current;

        public ProviderType Type => ProviderType.DummyText;

        object IDataProvider.Current => current;

        public void MoveNext(ISessionManager manager)
        {

        }

    }
}
