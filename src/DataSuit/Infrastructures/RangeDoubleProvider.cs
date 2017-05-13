using DataSuit.Enums;
using DataSuit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Infrastructures
{
    public class RangeDoubleProvider : IRangeProvider<double>
    {
        private double current, minValue, maxValue;
        public double Current => current;

        public ProviderType Type => ProviderType.Range;

        object IDataProvider.Current => current;

        public RangeDoubleProvider(int min, int max)
        {
            SetData(min, max);
            MoveNext();
        }

        public void MoveNext()
        {
            current = minValue + Utility.Rand.NextDouble() * (maxValue - minValue);
        }

        public void SetData(double min, double max)
        {
            if (minValue > maxValue)
                throw new ArgumentException($"{nameof(min)} can not be higher than {nameof(max)}");

            minValue = min;
            maxValue = max;
        }
    }
}
