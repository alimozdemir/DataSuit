using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;
using DataSuit.Infrastructures;

namespace DataSuit.Providers
{
    public class RangeIntProvider : IRangeProvider<int>
    {

        private int current, minValue, maxValue;
        public int Current => current;

        public ProviderType Type => ProviderType.Range;

        object IDataProvider.Current => current;

        public int MinValue => minValue;

        public int MaxValue => maxValue;
        public Type TType => typeof(int);

        public RangeIntProvider(int min, int max)
        {
            SetData(min, max);
            MoveNext();
        }

        public void MoveNext()
        {
            current = Utility.Rand.Next(minValue, maxValue);
        }

        public void SetData(int min, int max)
        {
            if (min > max)
                throw new ArgumentException($"{nameof(min)} can not be higher than {nameof(max)}");

            minValue = min;
            maxValue = max;
        }
    }
}
