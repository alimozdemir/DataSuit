using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataSuit.Enums;

namespace DataSuit.Infrastructures
{
    public class RangeIntProvider : IRangeProvider<int>
    {

        private int current, minValue, maxValue;
        public int Current => current;

        public ProviderType Type => ProviderType.Range;

        object IDataProvider.Current => current;
        
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
            if (minValue > maxValue)
                throw new ArgumentException($"{nameof(min)} can not be higher than {nameof(max)}");

            minValue = min;
            maxValue = max;
        }
    }
}
